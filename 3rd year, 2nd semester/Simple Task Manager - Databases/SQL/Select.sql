-- Afisare generala, ierarhica, a sarcinilor din baza de date corespondente utilizatorului „Dragos”. Sarcinile facute stau la baza tabelului, iar sortarea se face dupã prioritate.
-- Numele responsabilului e afisat doar daca reprezinta alta persoana decat utilizatorul in sine.
SELECT X, Pri, Sarcina, Proiect, Etichete, Dur_Est, Recurenta, Data_Prevazuta, Data_Limita, Data_Terminarii, Responsabil
  FROM afisare_generala
  WHERE Utilizator = 'Dragos';

-- Afisare generala a sarcinilor a caror parinte sau ele insele au ca eticheta 'proiecte'
SELECT X, Pri, Sarcina, Proiect, Etichete, Dur_Est, Recurenta, Data_Prevazuta, Data_Limita, Data_Terminarii, Responsabil
  FROM (
    SELECT distinct X, Pri, Sarcina, Proiect, Etichete, Dur_Est, Recurenta, Data_Prevazuta, Data_Limita, Data_Terminarii, Responsabil, Ordine, SYS_CONNECT_BY_PATH(etichete, ' #') as Etichete_Cale, Utilizator
      FROM afisare_generala
      CONNECT BY PRIOR id_sarcina = id_parinte 
      ORDER BY ordine
  ) WHERE etichete_cale LIKE '%proiecte%' AND Utilizator = 'Dragos';

-- Afisare generala a sarcinilor a caror parinte sau ele insele au ca proiect 'Facultate'
SELECT X, Pri, Sarcina, Proiect, Etichete, Dur_Est, Recurenta, Data_Prevazuta, Data_Limita, Data_Terminarii, Responsabil
  FROM (
    SELECT distinct X, Pri, Sarcina, Proiect, Etichete, Dur_Est, Recurenta, Data_Prevazuta, Data_Limita, Data_Terminarii, Responsabil, Ordine, SYS_CONNECT_BY_PATH(proiect, ' @') as Proiect_Cale, Utilizator
      FROM afisare_generala
      CONNECT BY PRIOR id_sarcina = id_parinte      
      ORDER BY ordine
  ) WHERE proiect_cale LIKE '%Facultate%' AND Utilizator = 'Dragos';

-- Afisat timpul estimat si timpul urmãrit la fiecare sarcinã a carui responsabil si utilizator e 'Dragos'. 
-- Se afiseaza doar sarcinile care au ambele atribute cu valoare nenula.
-- Se afiseaza de asemenea si eroarea de timp estimat-urmãrit.
SELECT s1.id, s1.sarcina, s1.durata_estimata, s2.durata_urmarita, (s2.durata_urmarita - s1.durata_estimata) as Eroare,
      ROUND(((EXTRACT (DAY FROM (s2.durata_urmarita)) * 86400
      + EXTRACT (HOUR FROM (s2.durata_urmarita)) * 3600
      + EXTRACT (MINUTE FROM (s2.durata_urmarita)) * 60
      + EXTRACT (SECOND FROM (s2.durata_urmarita))) /
      (EXTRACT (DAY FROM (s1.durata_estimata)) * 86400
      + EXTRACT (HOUR FROM (s1.durata_estimata)) * 3600
      + EXTRACT (MINUTE FROM (s1.durata_estimata)) * 60
      + EXTRACT (SECOND FROM (s1.durata_estimata)))) - 1, 2) as Eroare_Proc FROM (  
  SELECT s.id, s.id_responsabil, id_utilizator, CASE
      WHEN s.data_terminarii IS NULL THEN ' '
      ELSE 'X' END X,
      SYS_CONNECT_BY_PATH(descriere, ' -> ') as Sarcina,
      s.durata_estimata
    FROM sarcina s
    WHERE s.durata_estimata IS NOT null
    START WITH s.id_sarcina_parinte IS NULL
      CONNECT BY PRIOR s.id = s.id_sarcina_parinte
      ORDER SIBLINGS BY s.data_terminarii desc, s.prioritate
  ) s1 JOIN (
  SELECT
    s.id,
    NUMTODSINTERVAL(
      SUM(
        EXTRACT (DAY FROM (t.data_terminarii - t.data_inceperii)) * 86400
        + EXTRACT (HOUR FROM (t.data_terminarii - t.data_inceperii)) * 3600
        + EXTRACT (MINUTE FROM (t.data_terminarii - t.data_inceperii)) * 60
        + EXTRACT (SECOND FROM (t.data_terminarii - t.data_inceperii))
        ), 'SECOND') as Durata_urmarita
    FROM sarcina s
    JOIN timp_petrecut t
    ON t.id_sarcina = s.id
    GROUP BY s.id
  ) s2 ON s1.id = s2.id
  WHERE s1.id_responsabil = 1 AND s1.id_utilizator = 1;

-- Afisat ordinea calendaristica a sarcinilor, dupa datile prevazute si limita
SELECT distinct TO_CHAR(DATA, 'dd mon, hh24:mi') as Data, Tip,
  CASE
    WHEN s.data_terminarii IS NULL THEN ' '
    ELSE 'X' END X,
  SUBSTR(SYS_CONNECT_BY_PATH(s.descriere, ' -> '), 5) as Sarcina, LEVEL
  FROM (
    SELECT s1.data_prevazuta as Data, 'Prevazut' as Tip, s1.id as ID
      FROM sarcina s1
      WHERE s1.data_prevazuta IS NOT NULL AND s1.id_utilizator = 1
    UNION ALL
    SELECT s2.data_limita, 'Limita', s2.id
      FROM sarcina s2
      WHERE s2.data_limita IS NOT NULL AND s2.id_utilizator = 1
  ) sl
  JOIN sarcina s
  ON s.id = sl.ID
  START WITH s.id_sarcina_parinte IS NULL
      CONNECT BY PRIOR s.id = s.id_sarcina_parinte
  ORDER BY data, LEVEL;

-- Afisat sarcinile neterminate din ziua de 19 aprilie 2016
SELECT X, Pri, Sarcina, Proiect, Etichete, Dur_Est, Recurenta, 
    NVL(TO_CHAR(data_prevazuta, 'dd mon, hh24:mi'), ' ') as Data_prevazuta, 
    NVL(TO_CHAR(data_limita, 'dd mon, hh24:mi'), ' ') as Data_limita, 
    Responsabil
  FROM (
    SELECT ID_Sarcina, ID_Parinte, X, Pri, SUBSTR(SYS_CONNECT_BY_PATH(TRIM(Sarcina), ' -> '), 5) as Sarcina, CONNECT_BY_ROOT ID_Parinte ID_Parinte_Parinte, Proiect, Etichete, Dur_Est, Recurenta, Data_prevazuta, Data_limita, Responsabil
      FROM afisare_generala_aux a
      WHERE Utilizator = 'Dragos'
        AND ((Data_Prevazuta BETWEEN TO_DATE('19-04-2016 00:00', 'dd-mm-yyyy hh24:mi')
            AND TO_DATE('19-04-2016 23:59', 'dd-mm-yyyy hh24:mi'))
          OR (Data_Limita BETWEEN TO_DATE('19-04-2016 00:00', 'dd-mm-yyyy hh24:mi')
            AND TO_DATE('19-04-2016 23:59', 'dd-mm-yyyy hh24:mi')))
    CONNECT BY PRIOR ID_Sarcina = ID_Parinte
  ) WHERE (ID_Parinte_Parinte IS NULL OR ID_Parinte IS NULL) AND (X <> 'X');


-- Afisat sarcinile care sunt in asteptare
--- Prin sarcini in asteptare intelegem acele sarcini care nu au ca responsabil utilizatorul asociat
--- si care nu au data terminarii (nu sunt indeplinite)
SELECT X, Pri, Sarcina, Proiect, Etichete, Dur_Est, Recurenta, 
    NVL(TO_CHAR(data_prevazuta, 'dd mon, hh24:mi'), ' ') as Data_prevazuta, 
    NVL(TO_CHAR(data_limita, 'dd mon, hh24:mi'), ' ') as Data_limita, 
    Responsabil
  FROM (
    SELECT ID_Sarcina, ID_Parinte, X, Pri, SUBSTR(SYS_CONNECT_BY_PATH(TRIM(Sarcina), ' -> '), 5) as Sarcina, CONNECT_BY_ROOT ID_Parinte ID_Parinte_Parinte, Proiect, Etichete, Dur_Est, Recurenta, Data_prevazuta, Data_limita, Responsabil
      FROM afisare_generala_aux a
      WHERE Utilizator = 'Dragos'
    CONNECT BY PRIOR ID_Sarcina = ID_Parinte
  ) WHERE (ID_Parinte_Parinte IS NULL OR ID_Parinte IS NULL) AND (X <> 'X') AND (Responsabil <> ' ');
  
-- Afisat numarul de sarcini nefacute, facute si totalul lor grupat pe proiecte
SELECT p.nume as Proiect, COUNT(s.data_terminarii) as Sarcini_nefacute, COUNT(s.id) - COUNT(s.data_terminarii) as Sarcini_facute, COUNT(s.id) as Total_sarcini
  FROM sarcina s, proiect p
  WHERE s.id_proiect = p.id AND s.id_utilizator = 1
  GROUP BY p.nume;
  
-- Afisat sarcinile neglijate
--- Prin sarcini neglijate intelegem acele sarcini care nu au una dintre datile limita sau prevazuta
SELECT X, Pri, Sarcina, Proiect, Etichete, Dur_Est, Recurenta, Data_prevazuta, Data_limita, Responsabil
  FROM (
    SELECT ID_Sarcina, ID_Parinte, X, Pri, SUBSTR(SYS_CONNECT_BY_PATH(TRIM(Sarcina), ' -> '), 5) as Sarcina, CONNECT_BY_ROOT ID_Parinte ID_Parinte_Parinte, Proiect, Etichete, Dur_Est, Recurenta, 
    NVL(TO_CHAR(data_prevazuta, 'dd mon, hh24:mi'), ' ') as Data_prevazuta, 
    NVL(TO_CHAR(data_limita, 'dd mon, hh24:mi'), ' ') as Data_limita, 
    Responsabil
      FROM afisare_generala_aux a
      WHERE Utilizator = 'Dragos'
    CONNECT BY PRIOR ID_Sarcina = ID_Parinte
  ) WHERE (ID_Parinte_Parinte IS NULL OR ID_Parinte IS NULL) AND (Data_prevazuta = ' ') AND (Data_limita = ' ') AND (X <> 'X');

-- Afisat acele sarcini carora le se urmareste timpul in acest moment
-- In alte cuvinte, nu au o data si ora de sfarsit asociat in tabela Timpul_petrecut
SELECT s2.Descriere as Sarcina, Pri, Proiect, Etichete, Dur_Est, Data_inceperii as Contor_pornit_la
  FROM (
    SELECT Pri, Sarcina, Proiect, Etichete, Dur_Est, ID_Parinte, a.ID_Sarcina,
    NVL(TO_CHAR(t.data_inceperii, 'dd mon, hh24:mi'), ' ') as Data_inceperii, 
    NVL(TO_CHAR(t.data_terminarii, 'dd mon, hh24:mi'), ' ') as Data_terminarii
      FROM afisare_generala_aux a, timp_petrecut t
      WHERE Utilizator = 'Dragos' AND a.id_sarcina = t.id_sarcina    
  ) s1, (
    SELECT s.id, SUBSTR(SYS_CONNECT_BY_PATH(TRIM(s.Descriere), ' -> '), 5) as Descriere,
      CONNECT_BY_ROOT(s.id_sarcina_parinte) as ID_Parinte
      FROM sarcina s
      CONNECT BY PRIOR s.id = s.id_sarcina_parinte
    ) s2
  WHERE s1.ID_Sarcina = s2.ID AND s2.ID_Parinte IS NULL AND s1.Data_terminarii = ' ';
  
-- Afisat ordinea calendaristica a notificarilor si sarcinilor corespunzatoare
SELECT TO_CHAR(n.data_notificare, 'dd mon, hh24:mi') as Data,
  SUBSTR(SYS_CONNECT_BY_PATH(s.descriere, ' -> '), 5) as Sarcina, r.prenume as Responsabil
  FROM sarcina s, notificare n, responsabil r
  WHERE n.id_sarcina = s.id AND r.id = s.id_responsabil AND s.id_utilizator = 1
  CONNECT BY PRIOR s.id = s.id_sarcina_parinte
  ORDER BY Data;