Insert into RESPONSABIL (PRENUME, NUME) values ('Dragos', 'Perju'); 
Insert into UTILIZATOR (ID_RESPONSABIL, NUME_UTILIZATOR,PAROLA) values ('1', 'dragos','m0TM8un20gFjD0nDBPab8lxWEXpBfYRn220Ey2pO0x+2qGPKLJ84Fir9NPti2cfAfFnIxYMjtn2uCt945iTOeFElSuSW2PisRsewVZOQ1GA=');
Insert into PROIECT (NUME) values ('Treburi');
Insert into SARCINA (DESCRIERE,PRIORITATE,ID_PROIECT,ID_RESPONSABIL,ID_UTILIZATOR,DATA_CREARII,RECURENTA,DURATA_ESTIMATA,DATA_LIMITA,DATA_PREVAZUTA) 
  values (
    'Mers la cumparaturi', -- Descriere
    '3', -- Prioritate
    '1', -- ID_Proiect
    '1', -- ID_Responsabil
    '1', -- ID_Utilizator
    to_timestamp('19-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    INTERVAL '7' DAY, -- Recurenta
    NUMTODSINTERVAL(3, 'hour'), -- Durata estimata
    to_timestamp('24-04-2016 18:00', 'DD-MM-RRRR HH24:MI'), --Data limita
    to_timestamp('23-04-2016 10:00', 'DD-MM-RRRR HH24:MI') --Data prevazuta
    ); 

Insert into ETICHETA (NUME) values ('carrefour');
Insert into ETICHETA (NUME) values ('in_oras');

Insert into SARCINA_ETICHETA (ID_SARCINA,ID_ETICHETA) values ('1','1');
Insert into SARCINA_ETICHETA (ID_SARCINA,ID_ETICHETA) values ('1','2');

Insert into SARCINA (DESCRIERE,ID_RESPONSABIL,ID_UTILIZATOR,DATA_CREARII,ID_SARCINA_PARINTE)
  values (
    'Luat lapte', -- Descriere
    '1', -- ID_Responsabil
    '1', -- ID_Utilizator
    to_timestamp('19-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    '1' -- ID_Sarcina_Parinte
    );

Insert into SARCINA (DESCRIERE,ID_RESPONSABIL,ID_UTILIZATOR,DATA_CREARII,ID_SARCINA_PARINTE) 
  values (
    'Luat legume', -- Descriere
    '1', -- ID_Responsabil
    '1', -- ID_Utilizator
    to_timestamp('19-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    '1' -- ID_Sarcina_Parinte
    );

Insert into SARCINA (DESCRIERE,ID_RESPONSABIL,ID_UTILIZATOR,DATA_CREARII,ID_SARCINA_PARINTE) 
  values (
    'Luat fructe', -- Descriere
    '1', -- ID_Responsabil
    '1', -- ID_Utilizator
    to_timestamp('19-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    '1' -- ID_Sarcina_Parinte
    );
    
Insert into SARCINA (DESCRIERE,ID_RESPONSABIL,ID_UTILIZATOR,DATA_CREARII,ID_SARCINA_PARINTE) 
  values (
    'Rosii', -- Descriere
    '1', -- ID_Responsabil
    '1', -- ID_Utilizator
    to_timestamp('19-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    '3' -- ID_Sarcina_Parinte
    );
    
Insert into SARCINA (DESCRIERE,ID_RESPONSABIL,ID_UTILIZATOR,DATA_CREARII,ID_SARCINA_PARINTE) 
  values (
    'Castraveti', -- Descriere
    '1', -- ID_Responsabil
    '1', -- ID_Utilizator
    to_timestamp('19-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    '3' -- ID_Sarcina_Parinte
    );

Insert into PROIECT (NUME) values ('Facultate');

Insert into SARCINA (DESCRIERE,PRIORITATE,ID_PROIECT,ID_RESPONSABIL,ID_UTILIZATOR,DATA_CREARII,DATA_TERMINARII,DURATA_ESTIMATA,DATA_LIMITA,DATA_PREVAZUTA) 
  values (
    'Terminat tema casa BD', -- Descriere
    '2', -- Prioritate
    '2', -- ID_Proiect
    '1', -- ID_Responsabil
    '1', -- ID_Utilizator
    to_timestamp('16-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    to_timestamp('18-04-2016 13:00', 'DD-MM-RRRR HH24:MI'), -- Data terminarii
    NUMTODSINTERVAL(12, 'hour'), -- Durata estimata
    to_timestamp('18-04-2016 18:00', 'DD-MM-RRRR HH24:MI'), --Data limita
    to_timestamp('17-04-2016 10:00', 'DD-MM-RRRR HH24:MI') --Data prevazuta
    );
    
Insert into Eticheta(Nume) Values('proiecte');
Insert into Sarcina_Eticheta(ID_Sarcina, ID_Eticheta) VALUES('7', '3');

Insert into RESPONSABIL (PRENUME, NUME) values ('Stefan', 'Achirei'); 
Insert into SARCINA (DESCRIERE,DATA_CREARII,ID_PROIECT,ID_RESPONSABIL,ID_UTILIZATOR) 
  values (
    'Primit poze cu discutia despre proiect PSDp de la Stefan', -- Descriere
    to_timestamp('16-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    '2', -- ID_Proiect
    '2', -- ID_Responsabil
    '1' -- ID_Utilizator
    ); 
Insert into NOTIFICARE (ID_Sarcina, Data_notificare)
  values (
    '8', -- ID_Sarcina
    to_timestamp('20-04-2016 20:00', 'DD-MM-RRRR HH24:MI') -- Data notificare
  );
Insert into NOTIFICARE (ID_Sarcina, Data_notificare)
  values (
    '8', -- ID_Sarcina
    to_timestamp('21-04-2016 19:00', 'DD-MM-RRRR HH24:MI') -- Data notificare
  );

Insert into RESPONSABIL (PRENUME, NUME) values ('George', 'Uleru'); 
Insert into SARCINA (DESCRIERE,DATA_CREARII,ID_RESPONSABIL,ID_UTILIZATOR) 
  values (
    'Primit bani pentru coletul de la Optimus Digital comandat impreuna', -- Descriere
    to_timestamp('16-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    '3', -- ID_Responsabil
    '1' -- ID_Utilizator
    );
Insert into NOTIFICARE (ID_Sarcina, Data_notificare)
  values (
    '9', -- ID_Sarcina
    to_timestamp('21-04-2016 10:00', 'DD-MM-RRRR HH24:MI') -- Data notificare
  );
    
Insert into SARCINA (DESCRIERE,DATA_CREARII,DATA_TERMINARII,DATA_LIMITA,DATA_PREVAZUTA,PRIORITATE,ID_PROIECT,ID_RESPONSABIL,ID_UTILIZATOR) 
  values (
    'Lucrat la proiect BDp', -- Descriere
    to_timestamp('18-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    to_timestamp('20-04-2016 22:00', 'DD-MM-RRRR HH24:MI'), -- Data terminarii
    to_timestamp('27-04-2016 23:59', 'DD-MM-RRRR HH24:MI'), --Data limita
    to_timestamp('20-04-2016 23:59', 'DD-MM-RRRR HH24:MI'), --Data prevazuta
    '2', -- Prioritate
    '2', -- ID_Proiect
    '1', -- ID_Responsabil
    '1' -- ID_Utilizator
    );
Insert into Sarcina_Eticheta(ID_Sarcina, ID_Eticheta) VALUES('10', '3');

Insert into SARCINA (DESCRIERE,ID_SARCINA_PARINTE,ID_RESPONSABIL,ID_UTILIZATOR,DATA_CREARII,DATA_TERMINARII,DURATA_ESTIMATA,DATA_LIMITA,DATA_PREVAZUTA) 
  values (
    'Realizat structura logica', -- Descriere
    '10', -- ID_Sarcina_Parinte
    '1', -- ID_Responsabil
    '1', -- ID_Utilizator
    to_timestamp('18-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    to_timestamp('19-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data terminarii
    NUMTODSINTERVAL(3, 'hour'),
    to_timestamp('27-04-2016 23:59', 'DD-MM-RRRR HH24:MI'), --Data limita
    to_timestamp('19-04-2016 23:59', 'DD-MM-RRRR HH24:MI') --Data prevazuta
    );

Insert into SARCINA (DESCRIERE,ID_SARCINA_PARINTE,ID_RESPONSABIL,ID_UTILIZATOR,DATA_CREARII,DURATA_ESTIMATA,DATA_LIMITA,DATA_PREVAZUTA) 
  values (
    'Realizat structura relationala', -- Descriere
    '10', -- ID_Sarcina_Parinte
    '1', -- ID_Responsabil
    '1', -- ID_Utilizator
    to_timestamp('18-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    NUMTODSINTERVAL(2, 'hour'), -- Durata estimata
    to_timestamp('27-04-2016 23:59', 'DD-MM-RRRR HH24:MI'), --Data limita
    to_timestamp('19-04-2016 23:59', 'DD-MM-RRRR HH24:MI') --Data prevazuta
    ); 

Insert into SARCINA (DESCRIERE,ID_SARCINA_PARINTE,ID_RESPONSABIL,ID_UTILIZATOR,DATA_CREARII,DATA_TERMINARII,DURATA_ESTIMATA,DATA_LIMITA,DATA_PREVAZUTA) 
  values (
    'Realizat INSERT-uri', -- Descriere
    '10', -- ID_Sarcina_Parinte
    '1', -- ID_Responsabil
    '1', -- ID_Utilizator
    to_timestamp('18-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    to_timestamp('20-04-2016 12:00', 'DD-MM-RRRR HH24:MI'), -- Data terminarii
    NUMTODSINTERVAL(2, 'hour'), -- Durata estimata
    to_timestamp('27-04-2016 23:59', 'DD-MM-RRRR HH24:MI'), --Data limita
    to_timestamp('20-04-2016 23:59', 'DD-MM-RRRR HH24:MI') --Data prevazuta
    );

Insert into SARCINA (DESCRIERE,ID_SARCINA_PARINTE,ID_RESPONSABIL,ID_UTILIZATOR,DATA_CREARII,DURATA_ESTIMATA,DATA_LIMITA,DATA_PREVAZUTA) 
  values (
    'Realizat SELECT-uri', -- Descriere
    '10', -- ID_Sarcina_Parinte
    '1', -- ID_Responsabil
    '1', -- ID_Utilizator
    to_timestamp('18-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    NUMTODSINTERVAL(7, 'hour'), -- Durata estimata
    to_timestamp('27-04-2016 23:59', 'DD-MM-RRRR HH24:MI'), --Data limita
    to_timestamp('20-04-2016 23:59', 'DD-MM-RRRR HH24:MI') --Data prevazuta
    );

Insert into SARCINA (DESCRIERE,ID_SARCINA_PARINTE,ID_RESPONSABIL,ID_UTILIZATOR,DATA_CREARII,DATA_TERMINARII,DURATA_ESTIMATA,DATA_LIMITA,DATA_PREVAZUTA) 
  values (
    'Afisare generala', -- Descriere
    '14', -- ID_Sarcina_Parinte
    '1', -- ID_Responsabil
    '1', -- ID_Utilizator
    to_timestamp('18-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    to_timestamp('20-04-2016 14:00', 'DD-MM-RRRR HH24:MI'), -- Data terminarii
    NUMTODSINTERVAL(1, 'hour') + NUMTODSINTERVAL(30, 'minute'), -- Durata estimata
    to_timestamp('27-04-2016 23:59', 'DD-MM-RRRR HH24:MI'), --Data limita
    to_timestamp('20-04-2016 23:59', 'DD-MM-RRRR HH24:MI') --Data prevazuta
    );

Insert into SARCINA (DESCRIERE,ID_SARCINA_PARINTE,ID_RESPONSABIL,ID_UTILIZATOR,DATA_CREARII,DURATA_ESTIMATA,DATA_LIMITA,DATA_PREVAZUTA) 
  values (
    'Filtrat proiecte / etichete', -- Descriere
    '14', -- ID_Sarcina_Parinte
    '1', -- ID_Responsabil
    '1', -- ID_Utilizator
    to_timestamp('18-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    NUMTODSINTERVAL(1, 'hour'), -- Durata estimata
    to_timestamp('27-04-2016 23:59', 'DD-MM-RRRR HH24:MI'), --Data limita
    to_timestamp('20-04-2016 23:59', 'DD-MM-RRRR HH24:MI') --Data prevazuta
    );

Insert into SARCINA (DESCRIERE,ID_SARCINA_PARINTE,ID_RESPONSABIL,ID_UTILIZATOR,DATA_CREARII,DURATA_ESTIMATA,DATA_LIMITA,DATA_PREVAZUTA) 
  values (
    'Timp petrecut / Timp estimat', -- Descriere
    '14', -- ID_Sarcina_Parinte
    '1', -- ID_Responsabil
    '1', -- ID_Utilizator
    to_timestamp('18-04-2016 20:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    NUMTODSINTERVAL(1, 'hour'), -- Durata estimata
    to_timestamp('27-04-2016 23:59', 'DD-MM-RRRR HH24:MI'), --Data limita
    to_timestamp('20-04-2016 23:59', 'DD-MM-RRRR HH24:MI') --Data prevazuta
    );

COMMIT;

Insert into TIMP_PETRECUT(ID_SARCINA, DATA_INCEPERII, DATA_TERMINARII)
  values(
    '15',
    to_timestamp('20-04-2016 12:00', 'DD-MM-RRRR HH24:MI'), -- Data inceperii
    to_timestamp('20-04-2016 12:42', 'DD-MM-RRRR HH24:MI') -- Data terminarii
  );

Insert into TIMP_PETRECUT(ID_SARCINA, DATA_INCEPERII, DATA_TERMINARII)
  values(
    '15',
    to_timestamp('20-04-2016 13:02', 'DD-MM-RRRR HH24:MI'), -- Data inceperii
    to_timestamp('20-04-2016 13:51', 'DD-MM-RRRR HH24:MI') -- Data terminarii
  );

Insert into TIMP_PETRECUT(ID_SARCINA, DATA_INCEPERII, DATA_TERMINARII)
  values(
    '16',
    to_timestamp('20-04-2016 13:57', 'DD-MM-RRRR HH24:MI'), -- Data inceperii
    to_timestamp('20-04-2016 14:39', 'DD-MM-RRRR HH24:MI') -- Data terminarii
  );
  
Insert into TIMP_PETRECUT(ID_SARCINA, DATA_INCEPERII, DATA_TERMINARII)
  values(
    '16',
    to_timestamp('20-04-2016 14:44', 'DD-MM-RRRR HH24:MI'), -- Data inceperii
    to_timestamp('20-04-2016 15:34', 'DD-MM-RRRR HH24:MI') -- Data terminarii
  );
  
Insert into TIMP_PETRECUT(ID_SARCINA, DATA_INCEPERII, DATA_TERMINARII)
  values(
    '17',
    to_timestamp('20-04-2016 16:12', 'DD-MM-RRRR HH24:MI'), -- Data inceperii
    to_timestamp('20-04-2016 16:32', 'DD-MM-RRRR HH24:MI') -- Data terminarii
  );
  
Insert into TIMP_PETRECUT(ID_SARCINA, DATA_INCEPERII, DATA_TERMINARII)
  values(
    '17',
    to_timestamp('20-04-2016 16:48', 'DD-MM-RRRR HH24:MI'), -- Data inceperii
    to_timestamp('20-04-2016 17:22', 'DD-MM-RRRR HH24:MI') -- Data terminarii
  );

Insert into TIMP_PETRECUT(ID_SARCINA, DATA_INCEPERII)
  values(
    '15',
    to_timestamp('20-04-2016 17:26', 'DD-MM-RRRR HH24:MI') -- Data inceperii
  );
  
Insert into SARCINA (DESCRIERE,PRIORITATE,ID_RESPONSABIL,ID_UTILIZATOR,DATA_CREARII,DATA_TERMINARII,RECURENTA,DURATA_ESTIMATA,DATA_PREVAZUTA) 
  values (
    'Facut sport', -- Descriere
    '1', -- Prioritate
    '1', -- ID_Responsabil
    '1', -- ID_Utilizator
    to_timestamp('16-04-2016 08:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    to_timestamp('17-04-2016 10:00', 'DD-MM-RRRR HH24:MI'), -- Data terminarii
    INTERVAL '2' DAY, -- Recurenta
    NUMTODSINTERVAL(1, 'hour'), -- Durata estimata
    to_timestamp('17-04-2016 08:00', 'DD-MM-RRRR HH24:MI') -- Data prevazuta
    );

Insert into SARCINA (DESCRIERE,PRIORITATE,ID_RESPONSABIL,ID_UTILIZATOR,DATA_CREARII,RECURENTA,DURATA_ESTIMATA,DATA_PREVAZUTA) 
  values (
    'Facut sport', -- Descriere
    '1', -- Prioritate
    '1', -- ID_Responsabil
    '1', -- ID_Utilizator
    to_timestamp('17-04-2016 10:00', 'DD-MM-RRRR HH24:MI'), -- Data crearii
    INTERVAL '2' DAY, -- Recurenta
    NUMTODSINTERVAL(1, 'hour'), -- Durata estimata
    to_timestamp('19-04-2016 08:00', 'DD-MM-RRRR HH24:MI') -- Data prevazuta
    );

  COMMIT;