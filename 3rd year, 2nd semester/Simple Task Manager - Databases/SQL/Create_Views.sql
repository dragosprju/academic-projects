CREATE OR REPLACE VIEW afisare_generala_aux AS
SELECT distinct Ordine, X, Pri, Sarcina, Proiect, NVL(LISTAGG(e.nume, ', ') WITHIN GROUP (ORDER BY e.nume) OVER (PARTITION BY s.id), ' ') AS Etichete, 
  CASE r.prenume
    WHEN ur.prenume THEN ' '
    ELSE r.prenume END as Responsabil,
  ur.prenume as Utilizator,
  data_terminarii, recurenta, data_limita, data_prevazuta, dur_est, Nivel, s.ID as ID_Sarcina, ID_Sarcina_Parinte as ID_Parinte,
  s.ID_Utilizator, s.ID_Responsabil
  FROM ( 
    SELECT s.id, 
        s.id_sarcina_parinte,
        CASE
          WHEN s.data_terminarii IS NULL THEN ' '
          ELSE 'X' END X,
        NVL(CHR(ASCII('A') + Prioritate - 1), ' ') as Pri,
        LPAD(descriere, length(descriere) + LEVEL - 1, '  ') as Sarcina,
        NVL(p.nume, ' ') as Proiect,
        Data_terminarii,
        (CASE
          WHEN recurenta IS NULL THEN ' '
          WHEN recurenta = INTERVAL '0 00:00:00' DAY(1) TO SECOND(0) THEN ' '
          WHEN SUBSTR(TO_CHAR(recurenta), 12) = '00:00:00' THEN  REPLACE(SUBSTR(TO_CHAR(recurenta), 0, 10), '0', '') || 'd '
          ELSE REPLACE(SUBSTR(TO_CHAR(recurenta), 0, 10), '0', '') || 'd ' || SUBSTR(TO_CHAR(recurenta), 12) END) AS Recurenta,
        Data_limita,
        Data_prevazuta,
        (
          SELECT NVL(est_d || est_h || est_m, ' ') FROM (
              SELECT
                CASE
                  WHEN EXTRACT(DAY from durata_estimata)>0 THEN TO_CHAR(EXTRACT(DAY from durata_estimata)) || 'd '
                  ELSE '' END as est_d,
                CASE
                  WHEN EXTRACT(HOUR from durata_estimata) > 0 THEN TO_CHAR(EXTRACT(HOUR from durata_estimata)) || 'h '
                  ELSE '' END as est_h,
                CASE
                  WHEN EXTRACT(MINUTE from durata_estimata) > 0 THEN TO_CHAR(EXTRACT(MINUTE from durata_estimata)) || 'm '
                  ELSE '' END as est_m,
		s2.id as s2_id
              FROM sarcina s2
            ) WHERE s2_id = s.id
        ) as Dur_est,
        id_utilizator,
        id_responsabil,
        ROWNUM as Ordine,
        LEVEL as Nivel
      FROM sarcina s
      LEFT JOIN proiect p
        ON p.id = s.id_proiect
      START WITH s.id_sarcina_parinte IS NULL
        CONNECT BY PRIOR s.id = s.id_sarcina_parinte
        ORDER SIBLINGS BY data_terminarii desc, prioritate
    ) s
    LEFT JOIN sarcina_eticheta se
      ON s.id = se.id_sarcina
    LEFT JOIN eticheta e
      ON se.id_eticheta = e.id
    LEFT JOIN utilizator u
      ON s.id_utilizator = u.id_responsabil
    LEFT JOIN responsabil ur
      ON u.id_responsabil = ur.id
    LEFT JOIN responsabil r
      ON s.id_responsabil = r.id
    ORDER BY s.Ordine;  

CREATE OR REPLACE VIEW afisare_generala AS
SELECT X, Pri, Sarcina, Proiect, Etichete, Dur_Est, Recurenta, 
    NVL(TO_CHAR(data_prevazuta, 'dd mon, hh24:mi'), ' ') as Data_prevazuta, 
    NVL(TO_CHAR(data_limita, 'dd mon, hh24:mi'), ' ') as Data_limita, 
    NVL(TO_CHAR(data_terminarii, 'dd mon, hh24:mi'), ' ') as Data_terminarii, 
    Responsabil, Nivel, Ordine, s.ID_Sarcina, ID_Parinte, Utilizator 
  FROM afisare_generala_aux s;