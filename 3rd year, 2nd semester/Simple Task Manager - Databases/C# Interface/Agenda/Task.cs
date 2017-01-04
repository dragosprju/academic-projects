using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Agenda
{
    public class Task
    {
        public int ID;
        public string description;
        public int? priority;

        public string project;
        public List<string> tags;

        public DateTime creation_date;
        public DateTime? finish_date;
        public DateTime? due_date;

        public bool searchCurrentDueDate = true;
        public bool searchLowerThanDueDate = false;
        public bool searchLowerThanDueTime = false;
        public bool disregardDueTime = false;

        public bool searchWithoutDueDate = false;
        public bool searchWithoutDueTime = true;

        public Task()
        {
        }

        // SELECT ID, description, priority, creation_date, finish_date, due_date
        public Task(List<object> selectRow)
        {
            ID = Convert.ToInt32(selectRow[0]);
            description = Convert.ToString(selectRow[1]);
            priority = selectRow[2] is DBNull ? default(int?) : Convert.ToInt32(selectRow[2]);

            project = selectRow[3] is DBNull ? null : Convert.ToString(selectRow[3]);

            creation_date = DateTime.Parse(Convert.ToString(selectRow[4]));
            finish_date = selectRow[5] is DBNull ? default(DateTime?) : DateTime.Parse(Convert.ToString(selectRow[5]));
            due_date = selectRow[6] is DBNull ? default(DateTime?) : DateTime.Parse(Convert.ToString(selectRow[6]));
        }

        public void setTags(List<string> tags)
        {
            this.tags = tags;
        }

        public override string ToString()
        {
            string taskString = "";
            if (finish_date != null)
            {
                taskString += "(X)";
            }
            else
            {
                taskString += "( )";
            }

            if (priority != null)
            {
                char pri = 'A';
                pri += (char)(priority - 1);
                taskString += " [" + pri + "]";
            }

            taskString += " " + description;

            if (project != null)
            {
                taskString += " @" + project;
            }

            if (due_date != null)
            {
                taskString += " data:" + due_date.Value.ToString("dd.MM.yyyy");

                string time = due_date.Value.ToString("HH:mm");
                if (time != "00:00")
                {
                    taskString += " ora:" + due_date.Value.ToString("HH:mm");
                }
            }

            foreach (string tag in tags)
            {
                taskString += " #" + tag;
            }

            return taskString;
        }
    }
}
