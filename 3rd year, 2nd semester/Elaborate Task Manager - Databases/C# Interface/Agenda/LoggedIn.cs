using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Agenda
{
    public static class LoggedIn
    {
        public static Account connectedAccount;

        public static List<Task> getTasks()
        {
            List<List<object>> taskObjects = DatabaseWrapper.Select(
                "SELECT distinct sarcina.ID, descriere, prioritate, NVL2(sarcina.id_proiect, proiect.nume, NULL) as proiect, data_crearii, data_terminarii, data_programata " +
                    "FROM sarcina, proiect " +
                    "WHERE (sarcina.id_proiect IS NULL OR sarcina.id_proiect = proiect.id) AND sarcina.id_utilizator =" + LoggedIn.connectedAccount.ID.ToString() +
                    "ORDER BY prioritate");

            List<Task> toReturn = new List<Task>();
            foreach(List<object> taskObject in taskObjects)
            {
                Task newTask = new Task(taskObject);
                newTask.setTags(getTags(newTask.ID));
                toReturn.Add(newTask);                
            }

            return toReturn;
         }

        private static List<string> getTags(int taskID)
        {
            List<List<object>> tagObjects = DatabaseWrapper.Select(
                "SELECT eticheta.nume FROM sarcina, sarcina_eticheta, eticheta " +
                    "WHERE sarcina.id = " + taskID.ToString() + " AND sarcina.id = sarcina_eticheta.id_sarcina AND sarcina_eticheta.id_eticheta = eticheta.id");
            List<string> toReturn = new List<string>();

            foreach (List<object> tagObject in tagObjects)
            {
                toReturn.Add((string)tagObject[0]);
            }

            return toReturn;
        }

        public static void addTask(Task toAdd)
        {
            string doesItContainDescription = "";
            string doesItContainPriority = "";
            string doesItContainProject = "";
            //string doesItContainTags = "";
            string doesItContainDate = "";

            string description = "";
            string priority = "";
            string project = "";
            string date = "";
            List<string> tags = new List<string>();
            List<string> tagsToAdd = new List<string>();

            if(!String.IsNullOrWhiteSpace(toAdd.description))
            {
                doesItContainDescription = "descriere, ";
                description = "'" + toAdd.description + "', ";
            }

            if (toAdd.priority != default(int?))
            {
                doesItContainPriority = "prioritate, ";
                priority = toAdd.priority.ToString() + ", ";
            }

            if (!String.IsNullOrWhiteSpace(toAdd.project))
            {
                doesItContainProject = "id_proiect, ";
                List<List<object>> result = DatabaseWrapper.Select("SELECT id FROM proiect WHERE nume = '" + toAdd.project + "'");
                
                if(result.Count == 0)
                {
                    // Trebuie inserat
                    DatabaseWrapper.Execute("INSERT INTO proiect(nume) VALUES('" + toAdd.project + "')");
                }
                // Trebuie apoi tinut minte id
                int projectID = Convert.ToInt32(DatabaseWrapper.Select("SELECT id FROM proiect WHERE nume = '" + toAdd.project + "'")[0][0]);
                project = projectID.ToString() + ", ";
            }

            if (toAdd.tags != null && toAdd.tags.Count > 0)
            {
                foreach(string tag in toAdd.tags)
                {
                    int? tagID = default(int?);

                    List<List<object>> result = DatabaseWrapper.Select("SELECT id FROM eticheta WHERE nume = '" + tag + "'");

                    if (result.Count > 0)
                    {
                        tagID = Convert.ToInt32(result[0][0]);
                        tags.Add(tagID.ToString());
                    }
                    else
                    {
                        tagsToAdd.Add(tag);
                    }
                }                
            }

            if(toAdd.due_date != default(DateTime?))
            {
                doesItContainDate = "data_programata, ";
                date = "to_date('" + toAdd.due_date.Value.ToString("dd-MM-yyyy HH:mm") + "', 'dd-MM-yyyy hh24:mi'), ";
            }

            DatabaseWrapper.Execute("INSERT INTO sarcina(" + doesItContainDescription + doesItContainPriority + doesItContainProject + doesItContainDate + "id_utilizator) " +
                "VALUES(" + description + priority + project + date + connectedAccount.ID + ")");


            // Avem TASK ID
            int taskID = Convert.ToInt32(DatabaseWrapper.Select("SELECT MAX(id) FROM sarcina")[0][0]);

            foreach (string tag in tagsToAdd)
            {
                DatabaseWrapper.Execute("INSERT INTO eticheta(nume) VALUES('" + tag + "')");
                int tagID = Convert.ToInt32(DatabaseWrapper.Select("SELECT id FROM eticheta WHERE nume = '" + tag + "'")[0][0]);
                tags.Add(tagID.ToString());
            }

            foreach (string tag in tags)
            {
                List<List<object>> result = DatabaseWrapper.Select("SELECT id_sarcina, id_eticheta FROM sarcina_eticheta WHERE id_sarcina = " + taskID.ToString() + " AND id_eticheta = " + tag); 

                if (result.Count == 0)
                {
                    DatabaseWrapper.Execute("INSERT INTO sarcina_eticheta VALUES(" + taskID.ToString() + ", " + tag + ")");
                }
            }
        }

        public static void deleteTask(int ID)
        {
            DatabaseWrapper.Execute("DELETE FROM sarcina WHERE sarcina.id = " + ID.ToString());
            DatabaseWrapper.Execute("DELETE FROM eticheta WHERE eticheta.id IN(" +
                "SELECT eticheta.id FROM eticheta " +
                "WHERE eticheta.id NOT IN(SELECT distinct eticheta.id FROM eticheta, sarcina_eticheta WHERE eticheta.id = sarcina_eticheta.id_eticheta))");
            DatabaseWrapper.Execute("DELETE FROM proiect WHERE proiect.id " +
                "IN(SELECT proiect.id FROM proiect " +
                "WHERE proiect.id NOT IN (SELECT distinct sarcina.id_proiect FROM sarcina WHERE sarcina.id_proiect IS NOT NULL))");
        }

        public static List<Task> selectTasks(Task toSearch)
        {
            string doISearchForDescription = "";
            string doISearchForPriority = "";
            string doISearchForProject = "";
            string doISearchForTags1 = "";
            string doISearchForTags2 = "";         
            string doISearchForDate = "";
            string doISearchForTime = "";

            if (!String.IsNullOrWhiteSpace(toSearch.description))
            {
                doISearchForDescription = "AND LOWER(descriere) LIKE ";
                string[] searchTerms = toSearch.description.Split(' ');
                bool first = true;
                foreach (string term in searchTerms)
                {
                    if (first)
                    {
                        doISearchForDescription += " '%' ";
                        first = false;
                    }
                    doISearchForDescription += "|| LOWER('" + term + "') || '%' ";
                }
            }

            if (toSearch.priority != null)
            {
                if (toSearch.priority == -1)
                {
                    doISearchForPriority = "AND prioritate IS NULL ";
                }
                else
                {
                    doISearchForPriority = "AND prioritate = " + toSearch.priority.ToString() + " ";
                }                
            }

            if (toSearch.project != null)
            {
                if (toSearch.project == "-")
                {
                    doISearchForProject = "AND NVL2(sarcina.id_proiect, proiect.nume, NULL) IS NULL ";
                }
                else
                {
                    doISearchForProject = "AND LOWER(NVL2(sarcina.id_proiect, proiect.nume, NULL)) LIKE '%' || LOWER('" + toSearch.project + "') || '%' ";
                }
            }

            if (toSearch.tags != null && toSearch.tags.Count > 0)
            {
                toSearch.tags = toSearch.tags.Distinct().ToList();
                if (toSearch.tags.Count == 1 && toSearch.tags[0] == "-")
                {
                    doISearchForTags1 = ", sarcina_eticheta ";
                    doISearchForTags2 += "AND sarcina.id NOT IN(sarcina_eticheta.id_sarcina) ";
                }
                else
                {
                    doISearchForTags1 = ", sarcina_eticheta, eticheta ";
                    doISearchForTags2 = "AND (sarcina.id = sarcina_eticheta.id_sarcina AND sarcina_eticheta.id_eticheta = eticheta.id ";
                    foreach (string tag in toSearch.tags)
                    {
                        if (tag == "-")
                        {
                            doISearchForTags2 += "OR sarcina.id NOT IN(sarcina_eticheta.id_sarcina) ";
                        }
                        else
                        {
                            doISearchForTags2 += "AND eticheta.nume LIKE '%" + tag + "%' ";
                        }
                    }
                    doISearchForTags2 += ") ";
                }
            }

            if (toSearch.due_date != null || (toSearch.due_date == null && toSearch.searchWithoutDueDate && toSearch.searchWithoutDueTime))
            {
                doISearchForDate += "AND (";
                if(toSearch.searchWithoutDueDate && toSearch.searchWithoutDueTime)
                {
                    doISearchForDate += "data_programata IS NULL ";
                }
                else if (toSearch.searchWithoutDueTime)
                {
                    if (toSearch.searchCurrentDueDate)
                    {
                        doISearchForDate += "data_programata BETWEEN to_date('" + toSearch.due_date.Value.AddDays(-1).ToString("dd.MM.yyyy") + " 23:59" + "', 'dd.MM.yyyy hh24:mi') AND " +
                            "to_date('" + toSearch.due_date.Value.AddDays(1).ToString("dd.MM.yyyy") + " 00:00" + "', 'dd.MM.yyyy hh24:mi')";
                    }
                    else
                    {
                        string symbol = "";
                        if (toSearch.searchLowerThanDueDate)
                        {
                            symbol = "<";
                        }
                        else
                        {
                            symbol = ">";
                        }
                        doISearchForDate += "data_programata " + symbol + " to_date('" + toSearch.due_date.Value.ToString("dd.MM.yyyy") + " 00:00" + "', 'dd.MM.yyyy hh24:mi')";
                    }
                }
                else
                {
                    if (toSearch.searchCurrentDueDate)
                    {
                        doISearchForDate += "data_programata BETWEEN to_date('" + toSearch.due_date.Value.AddDays(-1).ToString("dd.MM.yyyy") + " 23:59" + "', 'dd.MM.yyyy hh24:mi') AND " +
                            "to_date('" + toSearch.due_date.Value.AddDays(1).ToString("dd.MM.yyyy") + " 00:00" + "', 'dd.MM.yyyy hh24:mi')";
                    }
                    else
                    {
                        string symbol = "";
                        if (toSearch.searchLowerThanDueDate)
                        {
                            symbol = "<";
                        }
                        else
                        {
                            symbol = ">";
                        }
                        doISearchForDate += "data_programata " + symbol + " to_date('" + toSearch.due_date.Value.ToString("dd.MM.yyyy") + " 00:00" + "', 'dd.MM.yyyy hh24:mi')";
                    }
                }
                doISearchForDate += ") ";
            }

            List<List<object>> taskObjects = new List<List<object>>();
            try
            {
                taskObjects = DatabaseWrapper.Select(
                "SELECT distinct sarcina.ID, descriere, prioritate, NVL2(sarcina.id_proiect, proiect.nume, NULL) as proiect, data_crearii, data_terminarii, data_programata " +
                    "FROM sarcina, proiect " + doISearchForTags1 +
                    "WHERE (sarcina.id_proiect IS NULL OR sarcina.id_proiect = proiect.id) AND sarcina.id_utilizator =" + LoggedIn.connectedAccount.ID.ToString() + " " +
                    doISearchForDescription +
                    doISearchForPriority +
                    doISearchForProject +
                    doISearchForTags2 +
                    doISearchForDate +
                    "ORDER BY prioritate");
            }
            catch (Exception) { }

            List<Task> toReturn = new List<Task>();
            foreach (List<object> taskObject in taskObjects)
            {
                Task newTask = new Task(taskObject);
                newTask.setTags(getTags(newTask.ID));
                toReturn.Add(newTask);
            }

            return toReturn;

        }

        public static Task extractTaskFromCommand(string command)
        {
            if (command.Contains(")"))
            {
                command = command.Split(')')[1].Trim();
            }

            if (command.Contains("]"))
            {
                command = command.Split(']')[1].Trim();
            }

            List<string> splitCommand = new List<string>(command.Split(' '));
            List<string> cmdPrefix = new List<string>();
            List<string> cmdSufix = new List<string>();
            string taskDescr = "";

            foreach (string word in splitCommand)
            {
                string[] components = Regex.Split(word, @"(?<=[@#:])");
                if (components.Length > 1)
                {
                    cmdPrefix.Add(components[0]);
                    if (components[0].ToLower().Equals("ora:"))
                    {
                        cmdSufix.Add(components[1] + components[2].TrimEnd(':'));
                    }
                    else
                    {
                        cmdSufix.Add(components[1]);
                    }
                    
                }         
                else
                {
                    taskDescr += word + " ";
                }      
            }
            taskDescr = taskDescr.TrimEnd();

            // Cumparat lapte #cumparaturi #carrefour #in_oras @treburi pri:B

            Task newTask = new Task();
            newTask.description = taskDescr;

            if (cmdPrefix.Contains("@"))
            {
                int index = cmdPrefix.IndexOf("@");
                newTask.project = cmdSufix[index];
            }

            if (cmdPrefix.Contains("#"))
            {
                newTask.tags = new List<string>();
                int lastIndex = -1;
                foreach (string prefix in cmdPrefix)
                {
                    if (prefix == "#")
                    {
                        lastIndex = cmdPrefix.IndexOf(prefix,lastIndex + 1);
                        newTask.tags.Add(cmdSufix[lastIndex]);
                    }
                }
            }

            if (cmdPrefix.FindAll(x => x.IndexOf("pri:", StringComparison.OrdinalIgnoreCase) >= 0).Count > 0 ||
                  cmdPrefix.FindAll(x => x.IndexOf("prioritate:", StringComparison.OrdinalIgnoreCase) >= 0).Count > 0)
            {
                int pri_index = cmdPrefix.FindIndex(x => x.Equals("pri:", StringComparison.OrdinalIgnoreCase));
                int priority_index = cmdPrefix.FindIndex(x => x.Equals("prioritate:", StringComparison.OrdinalIgnoreCase));
                int workingIndex = -1;

                if (priority_index == -1 || pri_index < priority_index)
                {
                    workingIndex = pri_index;
                }
                else
                {
                    workingIndex = priority_index;
                }

                if (workingIndex != -1 && cmdSufix[workingIndex].Length == 1)
                {
                    if ('A' <= cmdSufix[workingIndex][0] && cmdSufix[workingIndex][0] <= 'Z')
                    {
                        char c = cmdSufix[workingIndex][0];
                        c -= 'A';
                        c += (char)1;
                        newTask.priority = c;
                    }
                    else if (cmdSufix[workingIndex][0] == '-')
                    {
                        newTask.priority = -1;
                    }
                }
            }

            if (cmdPrefix.FindAll(x => x.IndexOf("data:", StringComparison.OrdinalIgnoreCase) >= 0).Count > 0)
            {
                int workingIndex = cmdPrefix.FindIndex(x => x.Equals("data:", StringComparison.OrdinalIgnoreCase));

                if (workingIndex != -1)
                {

                    if (cmdSufix[workingIndex][0] == '<')
                    {                        
                        newTask.searchCurrentDueDate = false;
                        newTask.searchLowerThanDueDate = true;
                        cmdSufix[workingIndex] = cmdSufix[workingIndex].Substring(1);
                    }
                    else if (cmdSufix[workingIndex][0] == '>')
                    {
                        newTask.searchCurrentDueDate = false;
                        newTask.searchLowerThanDueDate = false;
                        cmdSufix[workingIndex] = cmdSufix[workingIndex].Substring(1);
                    }
                    else if (cmdSufix[workingIndex].Length == 1 && cmdSufix[workingIndex][0] == '-')
                    {
                        newTask.searchWithoutDueDate = true;
                    }
                    else
                    {
                        newTask.searchCurrentDueDate = true;
                    }

                    if (newTask.due_date != null)
                    {
                        TimeSpan toAdd = default(TimeSpan);
                        TimeSpan.TryParse(cmdSufix[workingIndex], out toAdd);
                        if (toAdd != default(TimeSpan))
                        {
                            newTask.due_date += toAdd;
                        }                        
                    }
                    else
                    {
                        DateTime toAdd = default(DateTime);    
                        DateTime.TryParse(cmdSufix[workingIndex] + " 00:00", out toAdd);
                        if (toAdd != default(DateTime))
                        {
                            newTask.due_date = toAdd;
                        }                                             
                    }
                    
                }
            }

            if (cmdPrefix.FindAll(x => x.IndexOf("ora:", StringComparison.OrdinalIgnoreCase) >= 0).Count > 0)
            {
                int workingIndex = cmdPrefix.FindIndex(x => x.Equals("ora:", StringComparison.OrdinalIgnoreCase));

                if (workingIndex != -1)
                {
                    if (cmdSufix[workingIndex][0] == '<')
                    {
                        newTask.searchLowerThanDueTime = true;
                        newTask.disregardDueTime = false;
                        cmdSufix[workingIndex] = cmdSufix[workingIndex].Substring(1);
                    }
                    else if (cmdSufix[workingIndex][0] == '>')
                    {
                        newTask.searchLowerThanDueTime = false;
                        newTask.disregardDueTime = false;
                        cmdSufix[workingIndex] = cmdSufix[workingIndex].Substring(1);
                    }
                    else if (cmdSufix[workingIndex].Length == 1 && cmdSufix[workingIndex][0] == '-')
                    {
                        newTask.searchWithoutDueTime = true;
                    }
                    else
                    {
                        newTask.disregardDueTime = true;
                    }

                    if (newTask.due_date != null)
                    {
                        newTask.due_date += TimeSpan.Parse(cmdSufix[workingIndex]);
                    }
                    else
                    {
                        newTask.due_date = DateTime.Parse(cmdSufix[workingIndex]);
                    }

                }
            }
            return newTask;
        }

        public static void checkTask(int taskID)
        {
            List<List<object>> result = DatabaseWrapper.Select("SELECT data_terminarii FROM sarcina WHERE sarcina.id = " + taskID.ToString());

            if (!(result[0][0] is DBNull))
            {
                DatabaseWrapper.Execute("UPDATE sarcina SET data_terminarii = NULL WHERE sarcina.id = " + taskID.ToString());
            }
            else
            {
                DatabaseWrapper.Execute("UPDATE sarcina SET data_terminarii = SYSDATE WHERE sarcina.id = " + taskID.ToString());
            }

            
        }
    }
}
