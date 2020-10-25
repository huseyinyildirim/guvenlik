-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.5.7-rc-log


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema guvenlik
--

CREATE DATABASE IF NOT EXISTS guvenlik;
USE guvenlik;

--
-- Temporary table structure for view `depo_urunler`
--
DROP TABLE IF EXISTS `depo_urunler`;
DROP VIEW IF EXISTS `depo_urunler`;
CREATE TABLE `depo_urunler` (
  `id` int(11),
  `urun_id` int(11),
  `urun` varchar(510),
  `durum` int(1)
);

--
-- Temporary table structure for view `depo_urunler_say`
--
DROP TABLE IF EXISTS `depo_urunler_say`;
DROP VIEW IF EXISTS `depo_urunler_say`;
CREATE TABLE `depo_urunler_say` (
  `bolge_id` int(11),
  `firma_id` int(11),
  `urun_id` int(11),
  `urun` varchar(510),
  `durum` int(1),
  `tip` int(1),
  `adet` bigint(21)
);

--
-- Temporary table structure for view `depo_urunler_toplamlar`
--
DROP TABLE IF EXISTS `depo_urunler_toplamlar`;
DROP VIEW IF EXISTS `depo_urunler_toplamlar`;
CREATE TABLE `depo_urunler_toplamlar` (
  `bolge_id` int(11),
  `firma_id` int(11),
  `urun_id` int(11),
  `urun` varchar(510),
  `durum` int(11),
  `adet` decimal(33,0)
);

--
-- Definition of table `admin`
--

DROP TABLE IF EXISTS `admin`;
CREATE TABLE `admin` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bolge_id` int(11) NOT NULL DEFAULT '0',
  `firma_id` int(11) NOT NULL DEFAULT '0',
  `adi_soyadi` varchar(150) NOT NULL DEFAULT '',
  `kullanici_adi` varchar(150) NOT NULL DEFAULT '',
  `sifre` varchar(32) NOT NULL DEFAULT '',
  `tip` int(1) NOT NULL DEFAULT '0' COMMENT '0 Genel Yönetici (Root), 1 Bölge Yöneticisi, 2 Firma Yöneticisi, 3 Genel Depo Yöneticisi, 4 Firma Depo Yöneticisi',
  `admin_id` int(11) NOT NULL DEFAULT '0',
  `tarih` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `onay` int(1) NOT NULL DEFAULT '1' COMMENT '0 ise onaysız, 1 ise onaylı',
  PRIMARY KEY (`id`),
  KEY `id` (`id`),
  KEY `bolge_id` (`bolge_id`),
  KEY `firma_id` (`firma_id`),
  KEY `admin_id` (`admin_id`),
  KEY `tarih` (`tarih`),
  KEY `onay` (`onay`),
  KEY `tip` (`tip`)
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=latin5;

--
-- Dumping data for table `admin`
--

/*!40000 ALTER TABLE `admin` DISABLE KEYS */;
INSERT INTO `admin` (`id`,`bolge_id`,`firma_id`,`adi_soyadi`,`kullanici_adi`,`sifre`,`tip`,`admin_id`,`tarih`,`onay`) VALUES 
 (1,0,0,'TEST','test','D44A5662B22F07379CBBD55AE6611E12',0,1,'2010-06-27 14:31:54',1);
/*!40000 ALTER TABLE `admin` ENABLE KEYS */;


--
-- Definition of trigger `admin`
--

DROP TRIGGER /*!50030 IF EXISTS */ `admin`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `admin` BEFORE INSERT ON `admin` FOR EACH ROW BEGIN
SET NEW.tarih = NOW();
END $$

DELIMITER ;

--
-- Definition of table `bolge`
--

DROP TABLE IF EXISTS `bolge`;
CREATE TABLE `bolge` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `adi` varchar(500) NOT NULL DEFAULT '',
  `il_kodlari` varchar(243) NOT NULL DEFAULT '',
  `admin_id` int(11) NOT NULL DEFAULT '0',
  `tarih` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `onay` int(1) NOT NULL DEFAULT '1' COMMENT '0 ise onaysız, 1 ise onaylı',
  PRIMARY KEY (`id`),
  KEY `id` (`id`),
  KEY `admin_id` (`admin_id`),
  KEY `tarih` (`tarih`),
  KEY `onay` (`onay`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=latin5;

--
-- Dumping data for table `bolge`
--

/*!40000 ALTER TABLE `bolge` DISABLE KEYS */;
INSERT INTO `bolge` (`id`,`adi`,`il_kodlari`,`admin_id`,`tarih`,`onay`) VALUES 
 (1,'EGE','35,64',1,'2010-06-04 02:39:13',1),
 (2,'AKDENİZ','07',1,'2010-06-04 02:39:38',1),
 (3,'İÇ ANADOLU','06',1,'2010-06-04 02:40:14',1),
 (0,'MERKEZ','01,02,03,04,68,05,06,07,75,08,09,10,74,72,69,11,12,13,14,15,16,17,18,19,20,21,81,22,23,24,25,26,27,28,29,30,31,76,32,33,34,35,46,78,70,36,37,38,79,71,39,40,41,42,43,44,45,47,48,49,50,51,52,80,53,54,55,56,57,58,63,73,59,60,61,62,64,65,77,66,67',1,'2010-06-11 07:23:34',1);
/*!40000 ALTER TABLE `bolge` ENABLE KEYS */;


--
-- Definition of trigger `bolge`
--

DROP TRIGGER /*!50030 IF EXISTS */ `bolge`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `bolge` BEFORE INSERT ON `bolge` FOR EACH ROW BEGIN
SET NEW.tarih = NOW();
END $$

DELIMITER ;

--
-- Definition of table `depo`
--

DROP TABLE IF EXISTS `depo`;
CREATE TABLE `depo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bolge_id` int(11) NOT NULL DEFAULT '0' COMMENT 'sevk edildiyse ve proje zimmeti ise kullanılıyor',
  `firma_id` int(11) NOT NULL DEFAULT '0' COMMENT 'sevk edildiyse ve proje zimmeti ise kullanılıyor',
  `proje_id` int(11) NOT NULL DEFAULT '0' COMMENT 'proje zimmeti ise kullanılıyor',
  `personel_id` int(11) NOT NULL DEFAULT '0' COMMENT 'personel zimmeti ise kullanılıyor',
  `urun_id` int(11) NOT NULL DEFAULT '0',
  `durum` int(1) NOT NULL DEFAULT '1' COMMENT '0 ise eski, 1 ise yeni',
  `adet` int(11) NOT NULL DEFAULT '1',
  `tip` int(1) NOT NULL DEFAULT '0' COMMENT '0 depoya girme, 1 depodan sevk, 2 imha',
  `admin_id` int(11) NOT NULL DEFAULT '0',
  `tarih` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `onay` int(1) NOT NULL DEFAULT '1' COMMENT '0 ise onaysız, 1 ise onaylı',
  PRIMARY KEY (`id`),
  KEY `id` (`id`),
  KEY `tarih` (`tarih`),
  KEY `onay` (`onay`),
  KEY `admin_id` (`admin_id`),
  KEY `urun_id` (`urun_id`),
  KEY `bolge_id` (`bolge_id`),
  KEY `firma_id` (`firma_id`),
  KEY `durum` (`durum`),
  KEY `tip` (`tip`),
  KEY `proje_id` (`proje_id`),
  KEY `personel_id` (`personel_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin5 ROW_FORMAT=DYNAMIC;

--
-- Dumping data for table `depo`
--

/*!40000 ALTER TABLE `depo` DISABLE KEYS */;
/*!40000 ALTER TABLE `depo` ENABLE KEYS */;


--
-- Definition of trigger `depo1`
--

DROP TRIGGER /*!50030 IF EXISTS */ `depo1`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `depo1` BEFORE INSERT ON `depo` FOR EACH ROW BEGIN
SET NEW.tarih = NOW();

IF (NEW.adet = '') THEN
SET NEW.adet = '1';
END IF;

END $$

DELIMITER ;

--
-- Definition of trigger `depo2`
--

DROP TRIGGER /*!50030 IF EXISTS */ `depo2`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `depo2` BEFORE UPDATE ON `depo` FOR EACH ROW BEGIN
IF (OLD.adet = '') THEN
SET NEW.adet = '1';
END IF;
END $$

DELIMITER ;

--
-- Definition of table `firma`
--

DROP TABLE IF EXISTS `firma`;
CREATE TABLE `firma` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bolge_id` int(11) NOT NULL DEFAULT '0',
  `bolge_il_kodu` int(2) NOT NULL DEFAULT '0',
  `adi` varchar(500) NOT NULL DEFAULT '',
  `tel` varchar(13) NOT NULL DEFAULT '000 000 00 00',
  `faks` varchar(13) DEFAULT '000 000 00 00',
  `mail` varchar(320) DEFAULT NULL,
  `vergi_dairesi` varchar(80) DEFAULT NULL,
  `vergi_no` varchar(11) DEFAULT NULL,
  `ticaret_sicil_no` varchar(255) DEFAULT NULL,
  `il_kodu` int(2) NOT NULL DEFAULT '0',
  `ilce_kodu` int(3) NOT NULL DEFAULT '0',
  `adres` varchar(255) DEFAULT NULL,
  `admin_id` int(11) NOT NULL DEFAULT '0',
  `tarih` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `onay` int(1) NOT NULL DEFAULT '1' COMMENT '0 ise onaysız, 1 ise onaylı',
  PRIMARY KEY (`id`),
  KEY `id` (`id`),
  KEY `admin_id` (`admin_id`),
  KEY `tarih` (`tarih`),
  KEY `onay` (`onay`),
  KEY `il_kodu` (`il_kodu`),
  KEY `ilce_kodu` (`ilce_kodu`),
  KEY `bolge_id` (`bolge_id`),
  KEY `bolge_il_kodu` (`bolge_il_kodu`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=latin5;

--
-- Dumping data for table `firma`
--

/*!40000 ALTER TABLE `firma` DISABLE KEYS */;
INSERT INTO `firma` (`id`,`bolge_id`,`bolge_il_kodu`,`adi`,`tel`,`faks`,`mail`,`vergi_dairesi`,`vergi_no`,`ticaret_sicil_no`,`il_kodu`,`ilce_kodu`,`adres`,`admin_id`,`tarih`,`onay`) VALUES 
 (1,2,7,'HAS ÖZEL GÜVENLIK','000 000 00 00','','','','','',0,0,'',1,'2010-06-04 02:41:40',1),
 (2,1,64,'ULUSOY ÖZEL GÜVENLIK','000 000 00 00','','','','','',0,0,'',1,'2010-06-04 02:42:10',1),
 (3,3,6,'STK ÖZEL GÜVENLIK','000 000 00 00','','','','','',0,0,'',1,'2010-06-04 02:42:27',1),
 (0,0,0,'MERKEZ','000 000 00 00','','','','','',0,0,'',1,'2010-06-11 07:26:14',1);
/*!40000 ALTER TABLE `firma` ENABLE KEYS */;


--
-- Definition of trigger `firma1`
--

DROP TRIGGER /*!50030 IF EXISTS */ `firma1`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `firma1` BEFORE INSERT ON `firma` FOR EACH ROW BEGIN

SET NEW.tarih = NOW();

IF (NEW.mail = '') THEN
SET NEW.mail = NULL;
END IF;

IF (NEW.vergi_dairesi = '') THEN
SET NEW.vergi_dairesi = NULL;
END IF;

IF (NEW.vergi_no = '') THEN
SET NEW.vergi_no = NULL;
END IF;

IF (NEW.ticaret_sicil_no = '') THEN
SET NEW.ticaret_sicil_no = NULL;
END IF;

IF (NEW.adres = '') THEN
SET NEW.adres = NULL;
END IF;

END $$

DELIMITER ;

--
-- Definition of trigger `firma2`
--

DROP TRIGGER /*!50030 IF EXISTS */ `firma2`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `firma2` BEFORE UPDATE ON `firma` FOR EACH ROW BEGIN

IF (OLD.mail = '') THEN
SET NEW.mail = NULL;
END IF;

IF (OLD.vergi_dairesi = '') THEN
SET NEW.vergi_dairesi = NULL;
END IF;

IF (OLD.vergi_no = '') THEN
SET NEW.vergi_no = NULL;
END IF;

IF (OLD.ticaret_sicil_no = '') THEN
SET NEW.ticaret_sicil_no = NULL;
END IF;

IF (OLD.adres = '') THEN
SET NEW.adres = NULL;
END IF;

END $$

DELIMITER ;

--
-- Definition of table `il`
--

DROP TABLE IF EXISTS `il`;
CREATE TABLE `il` (
  `il_kodu` varchar(2) NOT NULL DEFAULT '00',
  `il` varchar(18) NOT NULL DEFAULT '',
  `alan_kodu` varchar(3) NOT NULL DEFAULT '000',
  `il_goster` int(1) NOT NULL DEFAULT '0',
  `tel_goster` int(1) NOT NULL DEFAULT '0',
  KEY `il_kodu` (`il_kodu`),
  KEY `il_goster` (`il_goster`),
  KEY `tel_goster` (`tel_goster`)
) ENGINE=MyISAM DEFAULT CHARSET=latin5 ROW_FORMAT=DYNAMIC;

--
-- Dumping data for table `il`
--

/*!40000 ALTER TABLE `il` DISABLE KEYS */;
INSERT INTO `il` (`il_kodu`,`il`,`alan_kodu`,`il_goster`,`tel_goster`) VALUES 
 ('02','Adıyaman','416',0,0),
 ('03','Afyon','272',0,0),
 ('04','Ağrı','472',0,0),
 ('68','Aksaray','382',0,0),
 ('05','Amasya','358',0,0),
 ('06','Ankara','312',0,0),
 ('07','Antalya','242',0,0),
 ('75','Ardahan','478',0,0),
 ('08','Artvin','466',0,0),
 ('09','Aydin','256',0,0),
 ('10','Balıkesir','266',0,0),
 ('74','Bartin','378',0,0),
 ('72','Batman','488',0,0),
 ('69','Bayburt','458',0,0),
 ('11','Bilecik','228',0,0),
 ('12','Bingöl','426',0,0),
 ('13','Bitlis','434',0,0),
 ('14','Bolu','374',0,0),
 ('15','Burdur','248',0,0),
 ('16','Bursa','224',0,0),
 ('17','Çanakkale','286',0,0),
 ('18','Çankırı','376',0,0),
 ('19','Çorum','364',0,0),
 ('20','Denizli','258',0,0),
 ('21','Diyarbakır','412',0,0),
 ('81','Düzce','374',0,0),
 ('22','Edirne','284',0,0),
 ('23','Elaziğ','424',0,0),
 ('24','Erzincan','446',0,0),
 ('25','Erzurum','442',0,0),
 ('26','Eskişehir','222',0,0),
 ('27','Gaziantep','342',0,0),
 ('28','Giresun','454',0,0),
 ('29','Gümüşhane','456',0,0),
 ('30','Hakkari','438',0,0),
 ('31','Hatay','326',0,0),
 ('76','Iğdır','476',0,0),
 ('32','Isparta','246',0,0),
 ('33','İçel','324',0,0),
 ('34','İstanbul','000',0,1),
 ('35','İzmir','232',0,0),
 ('46','Kahramanmaraş','370',0,0),
 ('78','Karabük','338',0,0),
 ('70','Karaman','474',0,0),
 ('36','Kars','366',0,0),
 ('37','Kastamonu','352',0,0),
 ('38','Kayseri','318',0,0),
 ('79','Kilis','288',0,0),
 ('71','Kirikkale','386',0,0),
 ('39','Kirklareli','348',0,0),
 ('40','Kirşehir','344',0,0),
 ('41','Kocaeli','262',0,0),
 ('42','Konya','332',0,0),
 ('43','Kütahya','274',0,0),
 ('44','Malatya','422',0,0),
 ('45','Manisa','236',0,0),
 ('47','Mardin','482',0,0),
 ('48','Muğla','252',0,0),
 ('49','Muş','436',0,0),
 ('50','Nevşehir','384',0,0),
 ('51','Niğde','388',0,0),
 ('52','Ordu','452',0,0),
 ('80','Osmaniye','328',0,0),
 ('53','Rize','464',0,0),
 ('54','Sakarya','264',0,0),
 ('55','Samsun','362',0,0),
 ('56','Siirt','484',0,0),
 ('57','Sinop','368',0,0),
 ('58','Sivas','346',0,0),
 ('63','Şanlıurfa','414',0,0),
 ('73','Şirnak','486',0,0),
 ('59','Tekirdağ','282',0,0),
 ('60','Tokat','356',0,0),
 ('61','Trabzon','462',0,0),
 ('62','Tunceli','428',0,0),
 ('64','Uşak','276',0,0),
 ('65','Van','432',0,0),
 ('77','Yalova','226',0,0),
 ('66','Yozgat','354',0,0),
 ('67','Zonguldak','372',0,0),
 ('01','Adana','322',0,0),
 ('00','İstanbul (Avrupa)','212',1,0),
 ('00','İstanbul (Anadolu)','216',1,0);
/*!40000 ALTER TABLE `il` ENABLE KEYS */;


--
-- Definition of table `ilce`
--

DROP TABLE IF EXISTS `ilce`;
CREATE TABLE `ilce` (
  `ilce_kodu` int(3) NOT NULL AUTO_INCREMENT,
  `il_kodu` varchar(2) NOT NULL DEFAULT '00',
  `ilce` varchar(14) NOT NULL DEFAULT '',
  `merkez` int(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ilce_kodu`),
  KEY `ilce_kodu` (`ilce_kodu`),
  KEY `il_kodu` (`il_kodu`),
  KEY `merkez` (`merkez`)
) ENGINE=MyISAM AUTO_INCREMENT=940 DEFAULT CHARSET=latin5 ROW_FORMAT=DYNAMIC;

--
-- Dumping data for table `ilce`
--

/*!40000 ALTER TABLE `ilce` DISABLE KEYS */;
INSERT INTO `ilce` (`ilce_kodu`,`il_kodu`,`ilce`,`merkez`) VALUES 
 (1,'01','Merkez',1),
 (2,'01','Aladag',0),
 (3,'01','Ceyhan',0),
 (4,'01','Feke',0),
 (5,'01','Imamoglu',0),
 (6,'01','Karaisali',0),
 (7,'01','Karatas',0),
 (8,'01','Kozan',0),
 (9,'01','Pozanti',0),
 (10,'01','Saimbeyli',0),
 (11,'01','Seyhan',0),
 (12,'01','Tufanbeyli',0),
 (13,'01','Yumurtalik',0),
 (14,'01','Yüregir',0),
 (15,'02','Merkez',1),
 (16,'02','Besni',0),
 (17,'02','Çelikhan',0),
 (18,'02','Gerger',0),
 (19,'02','Gölbasi',0),
 (20,'02','Kahta',0),
 (21,'02','Samsat',0),
 (22,'02','Sincik',0),
 (23,'02','Tut',0),
 (24,'03','Merkez',1),
 (25,'03','Basmakçi',0),
 (26,'03','Bayat',0),
 (27,'03','Bolvadin',0),
 (28,'03','Çobanlar',0),
 (29,'03','Çay',0),
 (30,'03','Dazkiri',0),
 (31,'03','Dinar',0),
 (32,'03','Emirdag',0),
 (33,'03','Evciler',0),
 (34,'03','Hocalar',0),
 (35,'03','Ihsaniye',0),
 (36,'03','Iscehisar',0),
 (37,'03','Kizilören',0),
 (38,'03','Sandikli',0),
 (39,'03','Sincanli',0),
 (40,'03','Sultandagi',0),
 (41,'03','Suhut',0),
 (42,'04','Merkez',1),
 (43,'04','Diyadin',0),
 (44,'04','Dogubeyazit',0),
 (45,'04','Eleskirt',0),
 (46,'04','Hamur',0),
 (47,'04','Patnos',0),
 (48,'04','Tasliçay',0),
 (49,'04','Tutak',0),
 (50,'05','Merkez',1),
 (51,'05','Göynücek',0),
 (52,'05','Gümüshacikö',0),
 (53,'05','Hamamözü',0),
 (54,'05','Merzifon',0),
 (55,'05','Suluova',0),
 (56,'05','Tasova',0),
 (57,'06','Merkez',1),
 (58,'06','Akyurt',0),
 (59,'06','Altindag',0),
 (60,'06','Ayas',0),
 (61,'06','Bala',0),
 (62,'06','Beypazari',0),
 (63,'06','Çamlidere',0),
 (64,'06','Çankaya',0),
 (65,'06','Çubuk',0),
 (66,'06','Elmadag',0),
 (67,'06','Etimesgut',0),
 (68,'06','Evren',0),
 (69,'06','Gölbasi',0),
 (70,'06','Güdül',0),
 (71,'06','Haymana',0),
 (72,'06','Kalecik',0),
 (73,'06','Kazan',0),
 (74,'06','Keçiören',0),
 (75,'06','Kizilcahama',0),
 (76,'06','Mamak',0),
 (77,'06','Nallihan',0),
 (78,'06','Polatli',0),
 (79,'06','Sincan',0),
 (80,'06','Sereflikoçh',0),
 (81,'06','Yenimahalle',0),
 (82,'07','Merkez',1),
 (83,'07','Akseki',0),
 (84,'07','Alanya',0),
 (85,'07','Demre',0),
 (86,'07','Elmali',0),
 (87,'07','Finike',0),
 (88,'07','Gazipasa',0),
 (89,'07','Gündogmus',0),
 (90,'07','Ibradi',0),
 (91,'07','Kale',0),
 (92,'07','Kas',0),
 (93,'07','Kemer',0),
 (94,'07','Korkuteli',0),
 (95,'07','Kumluca',0),
 (96,'07','Manavgat',0),
 (97,'07','Serik',0),
 (98,'08','Merkez',1),
 (99,'08','Ardanuç',0),
 (100,'08','Arhavi',0),
 (101,'08','Borçka',0),
 (102,'08','Hopa',0),
 (103,'08','Murgul',0),
 (104,'08','Savsat',0),
 (105,'08','Yusufeli',0),
 (106,'09','Merkez',1),
 (107,'09','Bozdogan',0),
 (108,'09','Buharkent',0),
 (109,'09','Çine',0),
 (110,'09','Germencik',0),
 (111,'09','Incirliova',0),
 (112,'09','Karacasu',0),
 (113,'09','Karpuzlu',0),
 (114,'09','Koçarli',0),
 (115,'09','Kösk',0),
 (116,'09','Kusadasi',0),
 (117,'09','Kuyucak',0),
 (118,'09','Nazilli',0),
 (119,'09','Söke',0),
 (120,'09','Sultanhisar',0),
 (121,'09','Yenihisar',0),
 (122,'09','Yenipazar',0),
 (123,'10','Merkez',1),
 (124,'10','Ayvalik',0),
 (125,'10','Akçay',0),
 (126,'10','Balya',0),
 (127,'10','Bandirma',0),
 (128,'10','Bigadiç',0),
 (129,'10','Burhaniye',0),
 (130,'10','Dursunbey',0),
 (131,'10','Edremit',0),
 (132,'10','Erdek',0),
 (133,'10','Gönen',0),
 (134,'10','Gömeç',0),
 (135,'10','Havran',0),
 (136,'10','Ivrindi',0),
 (137,'10','Kepsut',0),
 (138,'10','Manyas',0),
 (139,'10','Marmara',0),
 (140,'10','Savastepe',0),
 (141,'10','Sindirgi',0),
 (142,'10','Susurluk',0),
 (143,'11','Merkez',1),
 (144,'11','Bozöyük',0),
 (145,'11','Gölpazari',0),
 (146,'11','Inhisar',0),
 (147,'11','Osmaneli',0),
 (148,'11','Pazaryeri',0),
 (149,'11','Sögüt',0),
 (150,'11','Yenipazar',0),
 (151,'12','Merkez',1),
 (152,'12','Adakli',0),
 (153,'12','Genç',0),
 (154,'12','Karliova',0),
 (155,'12','Kigi',0),
 (156,'12','Solhan',0),
 (157,'12','Yayladere',0),
 (158,'12','Yedisu',0),
 (159,'13','Merkez',1),
 (160,'13','Adilcevaz',0),
 (161,'13','Ahlat',0),
 (162,'13','Güroymak',0),
 (163,'13','Hizan',0),
 (164,'13','Mutki',0),
 (165,'13','Tatvan',0),
 (166,'14','Merkez',1),
 (167,'14','Dörtdivan',0),
 (168,'14','Gerede',0),
 (169,'14','Göynük',0),
 (170,'14','Kibriscik',0),
 (171,'14','Mengen',0),
 (172,'14','Mudurnu',0),
 (173,'14','Seben',0),
 (174,'14','Yeniçaga',0),
 (175,'15','Merkez',1),
 (176,'15','Altinyayla',0),
 (177,'15','Aglasun',0),
 (178,'15','Bucak',0),
 (179,'15','Çavdir',0),
 (180,'15','Çeltikçi',0),
 (181,'15','Gölhisar',0),
 (182,'15','Karamanli',0),
 (183,'15','Kemer',0),
 (184,'15','Tefenni',0),
 (185,'15','Yesilova',0),
 (186,'16','Merkez',1),
 (187,'16','Büyükorhan',0),
 (188,'16','Gemlik',0),
 (189,'16','Gürsu',0),
 (190,'16','Harmancik',0),
 (191,'16','Inegöl',0),
 (192,'16','Iznik',0),
 (193,'16','Karacabey',0),
 (194,'16','Keles',0),
 (195,'16','Kestel',0),
 (196,'16','Mudanya',0),
 (197,'16','Mustafakema',0),
 (198,'16','Nilüfer',0),
 (199,'16','Orhaneli',0),
 (200,'16','Orhangazi',0),
 (201,'16','Osmangazi',0),
 (202,'16','Yenisehir',0),
 (203,'16','Yildirim',0),
 (204,'17','Merkez',1),
 (205,'17','Ayvacik',0),
 (206,'17','Bayramiç',0),
 (207,'17','Bozcaada',0),
 (208,'17','Biga',0),
 (209,'17','Çan',0),
 (210,'17','Eceabat',0),
 (211,'17','Ezine',0),
 (212,'17','Gelibolu',0),
 (213,'17','Gökçeada',0),
 (214,'17','Lapseki',0),
 (215,'17','Yenice',0),
 (216,'18','Merkez',1),
 (217,'18','Atkaracalar',0),
 (218,'18','Bayramören',0),
 (219,'18','Çerkes',0),
 (220,'18','Eldivan',0),
 (221,'18','Ilgaz',0),
 (222,'18','Kizilirmak',0),
 (223,'18','Korgun',0),
 (224,'18','Kursunlu',0),
 (225,'18','Orta',0),
 (226,'18','Ovacik',0),
 (227,'18','Sabanözü',0),
 (228,'18','Yaprakli',0),
 (229,'19','Merkez',1),
 (230,'19','Alaca',0),
 (231,'19','Bayat',0),
 (232,'19','Bogazkale',0),
 (233,'19','Dodurga',0),
 (234,'19','Iskilip',0),
 (235,'19','Kargi',0),
 (236,'19','Laçin',0),
 (237,'19','Mecitözü',0),
 (238,'19','Oguzlar',0),
 (239,'19','Ortaköy',0),
 (240,'19','Osmancik',0),
 (241,'19','Sungurlu',0),
 (242,'19','Ugurludag',0),
 (243,'20','Merkez',1),
 (244,'20','Acipayam',0),
 (245,'20','Akköy',0),
 (246,'20','Babadag',0),
 (247,'20','Baklan',0),
 (248,'20','Bekilli',0),
 (249,'20','Beyagaç',0),
 (250,'20','Buldan',0),
 (251,'20','Bozkurt',0),
 (252,'20','Çal',0),
 (253,'20','Çameli',0),
 (254,'20','Çardak',0),
 (255,'20','Çivril',0),
 (256,'20','Güney',0),
 (257,'20','Honaz',0),
 (258,'20','Kale',0),
 (259,'20','Sarayköy',0),
 (260,'20','Serinhisar',0),
 (261,'20','Tavas',0),
 (262,'21','Merkez',1),
 (263,'21','Bismil',0),
 (264,'21','Çermik',0),
 (265,'21','Çinar',0),
 (266,'21','Çüngüs',0),
 (267,'21','Dicle',0),
 (268,'21','Egil',0),
 (269,'21','Ergani',0),
 (270,'21','Hani',0),
 (271,'21','Hazro',0),
 (272,'21','Kocaköy',0),
 (273,'21','Kulp',0),
 (274,'21','Lice',0),
 (275,'21','Silvan',0),
 (276,'22','Merkez',1),
 (277,'22','Enez',0),
 (278,'22','Havsa',0),
 (279,'22','Ipsala',0),
 (280,'22','Kesan',0),
 (281,'22','Lalapasa',0),
 (282,'22','Meriç',0),
 (283,'22','Süloglu',0),
 (284,'22','Uzunköprü',0),
 (285,'23','Merkez',1),
 (286,'23','Agin',0),
 (287,'23','Alacakaya',0),
 (288,'23','Aricak',0),
 (289,'23','Baskil',0),
 (290,'23','Karakoçan',0),
 (291,'23','Keban',0),
 (292,'23','Kovancilar',0),
 (293,'23','Maden',0),
 (294,'23','Palu',0),
 (295,'23','Sivrice',0),
 (296,'24','Merkez',1),
 (297,'24','Çayirli',0),
 (298,'24','Iliç',0),
 (299,'24','Kemah',0),
 (300,'24','Kemaliye',0),
 (301,'24','Otlukbeli',0),
 (302,'24','Refahiye',0),
 (303,'24','Tercan',0),
 (304,'24','Üzümlü',0),
 (305,'25','Merkez',1),
 (306,'25','Askale',0),
 (307,'25','Çat',0),
 (308,'25','Hinis',0),
 (309,'25','Horasan',0),
 (310,'25','Ilica',0),
 (311,'25','Ispir',0),
 (312,'25','Karaçoban',0),
 (313,'25','Karayazi',0),
 (314,'25','Köprüköy',0),
 (315,'25','Narman',0),
 (316,'25','Oltu',0),
 (317,'25','Olur',0),
 (318,'25','Pasinler',0),
 (319,'25','Pazaryolu',0),
 (320,'25','Senkaya',0),
 (321,'25','Tekman',0),
 (322,'25','Tortum',0),
 (323,'25','Uzundere',0),
 (324,'26','Merkez',1),
 (325,'26','Alpu',0),
 (326,'26','Beylikova',0),
 (327,'26','Çifteler',0),
 (328,'26','Günyüzü',0),
 (329,'26','Han',0),
 (330,'26','Inönü',0),
 (331,'26','Mahmudiye',0),
 (332,'26','Mihalgazi',0),
 (333,'26','Mihaliççik',0),
 (334,'26','Saricakaya',0),
 (335,'26','Seyitgazi',0),
 (336,'26','Sivrihisar',0),
 (337,'27','Merkez',1),
 (338,'27','Araban',0),
 (339,'27','Islahiye',0),
 (340,'27','Kilis',0),
 (341,'27','Kargamis',0),
 (342,'27','Nizip',0),
 (343,'27','Nurdagi',0),
 (344,'27','Oguzeli',0),
 (345,'27','Sahinbey',0),
 (346,'27','Sehitkamil',0),
 (347,'27','Yavuzeli',0),
 (348,'28','Merkez',1),
 (349,'28','Alucra',0),
 (350,'28','Bulancak',0),
 (351,'28','Çamoluk',0),
 (352,'28','Çanakçi',0),
 (353,'28','Dereli',0),
 (354,'28','Dogankent',0),
 (355,'28','Espiye',0),
 (356,'28','Eynesil',0),
 (357,'28','Görele',0),
 (358,'28','Güce',0),
 (359,'28','Kesap',0),
 (360,'28','Piraziz',0),
 (361,'28','Sebinkarahisar',0),
 (362,'28','Tirebolu',0),
 (363,'28','Yaglidere',0),
 (364,'29','Merkez',1),
 (365,'29','Kelkit',0),
 (366,'29','Köse',0),
 (367,'29','Kürtün',0),
 (368,'29','Siran',0),
 (369,'29','Torul',0),
 (370,'30','Merkez',1),
 (371,'30','Çukurca',0),
 (372,'30','Semdinli',0),
 (373,'30','Yüksekova',0),
 (374,'31','Merkez',1),
 (375,'31','Altinözü',0),
 (376,'31','Belen',0),
 (377,'31','Dörtyol',0),
 (378,'31','Erzin',0),
 (379,'31','Hassa',0),
 (380,'31','Iskenderun',0),
 (381,'31','Kirikhan',0),
 (382,'31','Kumlu',0),
 (383,'31','Reyhanli',0),
 (384,'31','Samandagi',0),
 (385,'31','Yayladagi',0),
 (386,'32','Merkez',1),
 (387,'32','Aksu',0),
 (388,'32','Atabey',0),
 (389,'32','Egirdir',0),
 (390,'32','Gelendost',0),
 (391,'32','Gönen',0),
 (392,'32','Keçiborlu',0),
 (393,'32','Senirkent',0),
 (394,'32','Sütçüler',0),
 (395,'32','Sarkikaraag',0),
 (396,'32','Uluborlu',0),
 (397,'32','Yenisarbade',0),
 (398,'32','Yalvaç',0),
 (399,'33','Merkez',1),
 (400,'33','Anamur',0),
 (401,'33','Aydincik',0),
 (402,'33','Bozyazi',0),
 (403,'33','Çamliyayla',0),
 (404,'33','Erdemli',0),
 (405,'33','Gülnar',0),
 (406,'33','Mut',0),
 (407,'33','Silifke',0),
 (408,'33','Tarsus',0),
 (409,'34','Merkez',1),
 (410,'34','Adalar',0),
 (411,'34','Avcilar',0),
 (412,'34','Bagcilar',0),
 (413,'34','Bakirköy',0),
 (414,'34','Bahçelievle',0),
 (415,'34','Bayrampasa',0),
 (416,'34','Besiktas',0),
 (417,'34','Beykoz',0),
 (418,'34','Beyoglu',0),
 (419,'34','Büyükçekmece',0),
 (420,'34','Çatalca',0),
 (421,'34','Eminönü',0),
 (422,'34','Eyüp',0),
 (423,'34','Esenler',0),
 (424,'34','Fatih',0),
 (425,'34','Gaziosmanpasa',0),
 (426,'34','Güngören',0),
 (427,'34','Kadiköy',0),
 (428,'34','Kagithane',0),
 (429,'34','Kartal',0),
 (430,'34','Küçükçekmece',0),
 (431,'34','Maltepe',0),
 (432,'34','Pendik',0),
 (433,'34','Sariyer',0),
 (434,'34','Silivri',0),
 (435,'34','Sultanbeyli',0),
 (436,'34','Sile',0),
 (437,'34','Sisli',0),
 (438,'34','Tuzla',0),
 (439,'34','Ümraniye',0),
 (440,'34','Üsküdar',0),
 (441,'34','Yalova',0),
 (442,'34','Zeytinburnu',0),
 (443,'35','Merkez',1),
 (444,'35','Aliaga',0),
 (445,'35','Bayindir',0),
 (446,'35','Balçova',0),
 (447,'35','Bergama',0),
 (448,'35','Beydag',0),
 (449,'35','Bornova',0),
 (450,'35','Buca',0),
 (451,'35','Çesme',0),
 (452,'35','Çigli',0),
 (453,'35','Dikili',0),
 (454,'35','Foça',0),
 (455,'35','Gaziemir',0),
 (456,'35','Güzelbahçe',0),
 (457,'35','Karaburun',0),
 (458,'35','Karsiyaka',0),
 (459,'35','Kemalpasa',0),
 (460,'35','Kinik',0),
 (461,'35','Kiraz',0),
 (462,'35','Konak',0),
 (463,'35','Menderes',0),
 (464,'35','Menemen',0),
 (465,'35','Narlidere',0),
 (466,'35','Ödemis',0),
 (467,'35','Seferihisar',0),
 (468,'35','Selçuk',0),
 (469,'35','Tire',0),
 (470,'35','Torbali',0),
 (471,'35','Urla',0),
 (472,'36','Merkez',1),
 (473,'36','Akyaka',0),
 (474,'36','Arpaçay',0),
 (475,'36','Digor',0),
 (476,'36','Kagizman',0),
 (477,'36','Sarikamis',0),
 (478,'36','Selim',0),
 (479,'36','Susuz',0),
 (480,'37','Merkez',1),
 (481,'37','Abana',0),
 (482,'37','Agli',0),
 (483,'37','Araç',0),
 (484,'37','Azdavay',0),
 (485,'37','Bozkurt',0),
 (486,'37','Cide',0),
 (487,'37','Çatalzeytin',0),
 (488,'37','Daday',0),
 (489,'37','Devrekani',0),
 (490,'37','Doganyurt',0),
 (491,'37','Hanönü',0),
 (492,'37','Ihsangazi',0),
 (493,'37','Inebolu',0),
 (494,'37','Küre',0),
 (495,'37','Pinarbasi',0),
 (496,'37','Seydiler',0),
 (497,'37','Senpazar',0),
 (498,'37','Tasköprü',0),
 (499,'37','Tosya',0),
 (500,'38','Merkez',1),
 (501,'38','Akkisla',0),
 (502,'38','Bünyan',0),
 (503,'38','Develi',0),
 (504,'38','Felahiye',0),
 (505,'38','Hacilar',0),
 (506,'38','Incesu',0),
 (507,'38','Kocasinan',0),
 (508,'38','Melikgazi',0),
 (509,'38','Özvatan',0),
 (510,'38','Pinarbasi',0),
 (511,'38','Sarioglan',0),
 (512,'38','Sariz',0),
 (513,'38','Talas',0),
 (514,'38','Tomarza',0),
 (515,'38','Yahyali',0),
 (516,'38','Yesilhisar',0),
 (517,'39','Merkez',1),
 (518,'39','Babaeski',0),
 (519,'39','Demirköy',0),
 (520,'39','Kofçaz',0),
 (521,'39','Lüleburgaz',0),
 (522,'39','Pehlivanköy',0),
 (523,'39','Pinarhisar',0),
 (524,'39','Vize',0),
 (525,'40','Merkez',1),
 (526,'40','Akçakent',0),
 (527,'40','Akpinar',0),
 (528,'40','Boztepe',0),
 (529,'40','Çiçekdagi',0),
 (530,'40','Kaman',0),
 (531,'40','Mucur',0),
 (532,'41','Merkez',1),
 (533,'41','Darica',0),
 (534,'41','Gebze',0),
 (535,'41','Gölcük',0),
 (536,'41','Kandira',0),
 (537,'41','Karamürsel',0),
 (538,'41','Körfez',0),
 (539,'42','Merkez',1),
 (540,'42','Ahirli',0),
 (541,'42','Akören',0),
 (542,'42','Aksehir',0),
 (543,'42','Altinekin',0),
 (544,'42','Beysehir',0),
 (545,'42','Bozkir',0),
 (546,'42','Derebucak',0),
 (547,'42','Cihanbeyli',0),
 (548,'42','Çumra',0),
 (549,'42','Çeltik',0),
 (550,'42','Derbent',0),
 (551,'42','Doganhisar',0),
 (552,'42','Emirgazi',0),
 (553,'42','Eregli',0),
 (554,'42','Güneysinir',0),
 (555,'42','Halkapinar',0),
 (556,'42','Hadim',0),
 (557,'42','Hüyük',0),
 (558,'42','Ilgin',0),
 (559,'42','Kadinhani',0),
 (560,'42','Karapinar',0),
 (561,'42','Karatay',0),
 (562,'42','Kulu',0),
 (563,'42','Meram',0),
 (564,'42','Sarayönü',0),
 (565,'42','Selçuklu',0),
 (566,'42','Seydisehir',0),
 (567,'42','Taskent',0),
 (568,'42','Tuzlukçu',0),
 (569,'42','Yalihöyük',0),
 (570,'42','Yunak',0),
 (571,'43','Merkez',1),
 (572,'43','Altintas',0),
 (573,'43','Aslanapa',0),
 (574,'43','Cavdarhisar',0),
 (575,'43','Domaniç',0),
 (576,'43','Dumlupinar',0),
 (577,'43','Emet',0),
 (578,'43','Gediz',0),
 (579,'43','Hisarcik',0),
 (580,'43','Pazarlar',0),
 (581,'43','Simav',0),
 (582,'43','Saphane',0),
 (583,'43','Tavsanli',0),
 (584,'44','Merkez',1),
 (585,'44','Akçadag',0),
 (586,'44','Arapgir',0),
 (587,'44','Arguvan',0),
 (588,'44','Battalgazi',0),
 (589,'44','Darende',0),
 (590,'44','Dogansehir',0),
 (591,'44','Doganyol',0),
 (592,'44','Hekimhan',0),
 (593,'44','Kale',0),
 (594,'44','Kuluncak',0),
 (595,'44','Pötürge',0),
 (596,'44','Yazihan',0),
 (597,'44','Yesilyurt',0),
 (598,'45','Merkez',1),
 (599,'45','Ahmetli',0),
 (600,'45','Akhisar',0),
 (601,'45','Alasehir',0),
 (602,'45','Demirci',0),
 (603,'45','Gölmarmara',0),
 (604,'45','Gördes',0),
 (605,'45','Kirkagaç',0),
 (606,'45','Köprübasi',0),
 (607,'45','Kula',0),
 (608,'45','Salihli',0),
 (609,'45','Sarigöl',0),
 (610,'45','Saruhanli',0),
 (611,'45','Selendi',0),
 (612,'45','Soma',0),
 (613,'45','Turgutlu',0),
 (614,'46','Merkez',1),
 (615,'46','Afsin',0),
 (616,'46','Andirin',0),
 (617,'46','Çaglayancer',0),
 (618,'46','Ekinözü',0),
 (619,'46','Elbistan',0),
 (620,'46','Göksun',0),
 (621,'46','Nurhak',0),
 (622,'46','Pazarcik',0),
 (623,'46','Türkoglu',0),
 (624,'47','Merkez',1),
 (625,'47','Dargeçit',0),
 (626,'47','Derik',0),
 (627,'47','Kiziltepe',0),
 (628,'47','Mazidagi',0),
 (629,'47','Midyat',0),
 (630,'47','Nusaybin',0),
 (631,'47','Ömerli',0),
 (632,'47','Savur',0),
 (633,'47','Yesilli',0),
 (634,'48','Merkez',1),
 (635,'48','Bodrum',0),
 (636,'48','Dalaman',0),
 (637,'48','Datça',0),
 (638,'48','Fethiye',0),
 (639,'48','Kavaklidere',0),
 (640,'48','Köycegiz',0),
 (641,'48','Marmaris',0),
 (642,'48','Milas',0),
 (643,'48','Ortaca',0),
 (644,'48','Ula',0),
 (645,'48','Yatagan',0),
 (646,'49','Merkez',1),
 (647,'49','Bulanik',0),
 (648,'49','Hasköy',0),
 (649,'49','Korkut',0),
 (650,'49','Malazgirt',0),
 (651,'49','Varto',0),
 (652,'50','Merkez',1),
 (653,'50','Acigöl',0),
 (654,'50','Avanos',0),
 (655,'50','Derinkuyu',0),
 (656,'50','Gülsehir',0),
 (657,'50','Hacibektas',0),
 (658,'50','Kozakli',0),
 (659,'50','Ürgüp',0),
 (660,'51','Merkez',1),
 (661,'51','Altunhisar',0),
 (662,'51','Bor',0),
 (663,'51','Çamardi',0),
 (664,'51','Çiftlik',0),
 (665,'51','Ulukisla',0),
 (666,'52','Merkez',1),
 (667,'52','Akkus',0),
 (668,'52','Aybasti',0),
 (669,'52','Çamas',0),
 (670,'52','Çatalpinar',0),
 (671,'52','Çaybasi',0),
 (672,'52','Fatsa',0),
 (673,'52','Gölköy',0),
 (674,'52','Gölyali',0),
 (675,'52','Gürgentepe',0),
 (676,'52','Ikizce',0),
 (677,'52','Korgan',0),
 (678,'52','Kabadüz',0),
 (679,'52','Kabatas',0),
 (680,'52','Kumru',0),
 (681,'52','Mesudiye',0),
 (682,'52','Persembe',0),
 (683,'52','Ulubey',0),
 (684,'52','Ünye',0),
 (685,'53','Merkez',1),
 (686,'53','Ardesen',0),
 (687,'53','Çamlihemsin',0),
 (688,'53','Çayeli',0),
 (689,'53','Derepazari',0),
 (690,'53','Findikli',0),
 (691,'53','Güneysu',0),
 (692,'53','Hemsin',0),
 (693,'53','Ikizdere',0),
 (694,'53','Iyidere',0),
 (695,'53','Kalkandere',0),
 (696,'53','Pazar',0),
 (697,'54','Merkez',1),
 (698,'54','Akyazi',0),
 (699,'54','Ferizli',0),
 (700,'54','Geyve',0),
 (701,'54','Hendek',0),
 (702,'54','Karapürçek',0),
 (703,'54','Karasu',0),
 (704,'54','Kaynarca',0),
 (705,'54','Kocaali',0),
 (706,'54','Pamukova',0),
 (707,'54','Sapanca',0),
 (708,'54','Sögütlü',0),
 (709,'54','Tarakli',0),
 (710,'55','Merkez',1),
 (711,'55','Alaçam',0),
 (712,'55','Asarcik',0),
 (713,'55','Ayvacik',0),
 (714,'55','Bafra',0),
 (715,'55','Çarsamba',0),
 (716,'55','Havza',0),
 (717,'55','Kavak',0),
 (718,'55','Ladik',0),
 (719,'55','19mayis',0),
 (720,'55','Salipazari',0),
 (721,'55','Tekkeköy',0),
 (722,'55','Terme',0),
 (723,'55','Vezirköprü',0),
 (724,'55','Yakakent',0),
 (725,'56','Merkez',1),
 (726,'56','Aydinlar',0),
 (727,'56','Baykan',0),
 (728,'56','Eruh',0),
 (729,'56','Kozluk',0),
 (730,'56','Kurtalan',0),
 (731,'56','Pervari',0),
 (732,'56','Sirvan',0),
 (733,'57','Merkez',1),
 (734,'57','Ayancik',0),
 (735,'57','Boyabat',0),
 (736,'57','Dikmen',0),
 (737,'57','Duragan',0),
 (738,'57','Erfelek',0),
 (739,'57','Gerze',0),
 (740,'57','Saraydüzü',0),
 (741,'57','Türkeli',0),
 (742,'58','Merkez',1),
 (743,'58','Akincilar',0),
 (744,'58','Altinyayla',0),
 (745,'58','Divrigi',0),
 (746,'58','Dogansar',0),
 (747,'58','Gemerek',0),
 (748,'58','Gölova',0),
 (749,'58','Gürün',0),
 (750,'58','Hafik',0),
 (751,'58','Imranli',0),
 (752,'58','Kangal',0),
 (753,'58','Koyulhisar',0),
 (754,'58','Susehri',0),
 (755,'58','Sarkisla',0),
 (756,'58','Ulas',0),
 (757,'58','Yildizeli',0),
 (758,'58','Zara',0),
 (759,'59','Merkez',1),
 (760,'59','Çerkezköy',0),
 (761,'59','Çorlu',0),
 (762,'59','Hayrabolu',0),
 (763,'59','Malkara',0),
 (764,'59','Marmaraeregli',0),
 (765,'59','Muratli',0),
 (766,'59','Saray',0),
 (767,'59','Sarköy',0),
 (768,'60','Merkez',1),
 (769,'60','Almus',0),
 (770,'60','Artova',0),
 (771,'60','Basçiftlik',0),
 (772,'60','Erbaa',0),
 (773,'60','Niksar',0),
 (774,'60','Pazar',0),
 (775,'60','Resadiye',0),
 (776,'60','Sulusaray',0),
 (777,'60','Turhal',0),
 (778,'60','Yesilyurt',0),
 (779,'60','Zile',0),
 (780,'61','Merkez',1),
 (781,'61','Akçaabat',0),
 (782,'61','Arakli',0),
 (783,'61','Arsin',0),
 (784,'61','Besikdüzü',0),
 (785,'61','Çarsibasi',0),
 (786,'61','Çaykara',0),
 (787,'61','Dernekpazar',0),
 (788,'61','Düzköy',0),
 (789,'61','Hayrat',0),
 (790,'61','Köprübasi',0),
 (791,'61','Maçka',0),
 (792,'61','Of',0),
 (793,'61','Sürmene',0),
 (794,'61','Salpazari',0),
 (795,'61','Tonya',0),
 (796,'61','Vakfikebir',0),
 (797,'61','Yomra',0),
 (798,'62','Merkez',1),
 (799,'62','Çemisgezek',0),
 (800,'62','Hozat',0),
 (801,'62','Mazgirt',0),
 (802,'62','Nazimiye',0),
 (803,'62','Ovacik',0),
 (804,'62','Pertek',0),
 (805,'62','Pülümür',0),
 (806,'63','Merkez',1),
 (807,'63','Akçakale',0),
 (808,'63','Birecik',0),
 (809,'63','Bozova',0),
 (810,'63','Ceylanpinar',0),
 (811,'63','Halfeti',0),
 (812,'63','Harran',0),
 (813,'63','Hilvan',0),
 (814,'63','Siverek',0),
 (815,'63','Suruç',0),
 (816,'63','Viransehir',0),
 (817,'64','Merkez',1),
 (818,'64','Banaz',0),
 (819,'64','Esme',0),
 (820,'64','Karahalli',0),
 (821,'64','Sivasli',0),
 (822,'64','Ulubey',0),
 (823,'65','Merkez',1),
 (824,'65','Bahçesaray',0),
 (825,'65','Baskale',0),
 (826,'65','Çaldiran',0),
 (827,'65','Çatak',0),
 (828,'65','Edremit',0),
 (829,'65','Ercis',0),
 (830,'65','Gevas',0),
 (831,'65','Gürpinar',0),
 (832,'65','Muradiye',0),
 (833,'65','Özalp',0),
 (834,'65','Saray',0),
 (835,'66','Merkez',1),
 (836,'66','Akdagmadeni',0),
 (837,'66','Aydincik',0),
 (838,'66','Bogazliyan',0),
 (839,'66','Çandir',0),
 (840,'66','Çayiralan',0),
 (841,'66','Çekerek',0),
 (842,'66','Kadisehri',0),
 (843,'66','Sarikaya',0),
 (844,'66','Saraykent',0),
 (845,'66','Sorgun',0),
 (846,'66','Sefaatli',0),
 (847,'66','Yenifakili',0),
 (848,'66','Yerköy',0),
 (849,'67','Merkez',1),
 (850,'67','Alapli',0),
 (851,'67','Çamoluk',0),
 (852,'67','Çaycuma',0),
 (853,'67','Devrek',0),
 (854,'67','Eflani',0),
 (855,'67','Eregli',0),
 (856,'67','Gökçebey',0),
 (857,'68','Merkez',1),
 (858,'68','Agaçören',0),
 (859,'68','Eskil',0),
 (860,'68','Gülagaç',0),
 (861,'68','Güzelyurt',0),
 (862,'68','Ortaköy',0),
 (863,'68','Sariyahsi',0),
 (864,'69','Merkez',1),
 (865,'69','Aydintepe',0),
 (866,'69','Demirözü',0),
 (867,'70','Merkez',1),
 (868,'70','Ayranci',0),
 (869,'70','Basyayla',0),
 (870,'70','Ermenek',0),
 (871,'70','Kazimkarabekir',0),
 (872,'70','Sariveliler',0),
 (873,'71','Merkez',1),
 (874,'71','Bahsili',0),
 (875,'71','Bagliseyh',0),
 (876,'71','Çelebi',0),
 (877,'71','Delice',0),
 (878,'71','Karakeçili',0),
 (879,'71','Keskin',0),
 (880,'71','Sulakyurt',0),
 (881,'71','Yahsihan',0),
 (882,'72','Merkez',1),
 (883,'72','Gercüs',0),
 (884,'72','Hasankeyf',0),
 (885,'72','Besiri',0),
 (886,'72','Kozluk',0),
 (887,'72','Sason',0),
 (888,'73','Merkez',1),
 (889,'73','Beytüsseba',0),
 (890,'73','Uludere',0),
 (891,'73','Cizre',0),
 (892,'73','Idil',0),
 (893,'73','Silopi',0),
 (894,'73','Güçlükonak',0),
 (895,'74','Merkez',1),
 (896,'74','Amasra',0),
 (897,'74','Kurucasile',0),
 (898,'74','Ulus',0),
 (899,'75','Merkez',1),
 (900,'75','Çildir',0),
 (901,'75','Damal',0),
 (902,'75','Göle',0),
 (903,'75','Hanak',0),
 (904,'75','Posof',0),
 (905,'76','Merkez',1),
 (906,'76','Aralik',0),
 (907,'76','Karakoyunlu',0),
 (908,'76','Tuzluca',0),
 (909,'77','Merkez',1),
 (910,'77','Altinova',0),
 (911,'77','Armutlu',0),
 (912,'77','Cinarcik',0),
 (913,'77','Ciftlikkoy',0),
 (914,'77','Termal',0),
 (915,'78','Merkez',1),
 (916,'78','Eflani',0),
 (917,'78','Eskipazar',0),
 (918,'78','Ovacik',0),
 (919,'78','Safranbolu',0),
 (920,'78','Yenice',0),
 (921,'79','Merkez',1),
 (922,'79','Elbeyli',0),
 (923,'79','Musabeyli',0),
 (924,'79','Polateli',0),
 (925,'80','Merkez',1),
 (926,'80','Bahçe',0),
 (927,'80','Hasanbeyli',0),
 (928,'80','Düziçi',0),
 (929,'80','Kadirli',0),
 (930,'80','Sunbas',0),
 (931,'80','Toprakkale',0),
 (932,'81','Merkez',1),
 (933,'81','Akçakoca',0),
 (934,'81','Cumayeri',0),
 (935,'81','Çilimli',0),
 (936,'81','Gölyaka',0),
 (937,'81','Gümüsova',0),
 (938,'81','Kaynasli',0),
 (939,'81','Yigilca',0);
/*!40000 ALTER TABLE `ilce` ENABLE KEYS */;


--
-- Definition of table `personel`
--

DROP TABLE IF EXISTS `personel`;
CREATE TABLE `personel` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bolge_id` int(11) NOT NULL DEFAULT '0',
  `firma_id` int(11) NOT NULL DEFAULT '0',
  `tc_kimlik` varchar(11) NOT NULL DEFAULT '',
  `adi_soyadi` varchar(150) NOT NULL DEFAULT '',
  `cinsiyet` int(1) DEFAULT '0' COMMENT '0 erkek,1 bayan',
  `tel` varchar(13) DEFAULT '000 000 00 00',
  `mail` varchar(320) DEFAULT NULL,
  `dogum_tarihi` date DEFAULT NULL,
  `dogum_yeri` varchar(255) DEFAULT NULL,
  `ana_adi` varchar(255) DEFAULT NULL,
  `baba_adi` varchar(255) DEFAULT NULL,
  `ssk_no` varchar(13) DEFAULT NULL,
  `egitim_durumu` int(1) DEFAULT '9' COMMENT '0 Eğitimsiz,1 Okul Öncesi,2 İlköğretim,3 Lise,4 Yüksek Okul,5 Üniversite,6 Yüksek Lisans,7 Doktora',
  `ehliyet` varchar(20) DEFAULT NULL,
  `sertifika_durumu` int(1) NOT NULL DEFAULT '0' COMMENT '0 Silahsız,1 Silahlı',
  `il_kodu` int(2) NOT NULL DEFAULT '0',
  `ilce_kodu` int(3) NOT NULL DEFAULT '0',
  `adres` varchar(150) DEFAULT '',
  `notlar` longtext,
  `admin_id` int(11) NOT NULL DEFAULT '0',
  `tarih` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `onay` int(1) NOT NULL DEFAULT '1' COMMENT '0 ise onaysız, 1 ise onaylı',
  PRIMARY KEY (`id`),
  UNIQUE KEY `tc_kimlik` (`tc_kimlik`),
  KEY `il_kodu` (`il_kodu`),
  KEY `admin_id` (`admin_id`),
  KEY `tarih` (`tarih`),
  KEY `onay` (`onay`),
  KEY `ilce_kodu` (`ilce_kodu`),
  KEY `bolge_id` (`bolge_id`),
  KEY `firma_id` (`firma_id`),
  KEY `id` (`id`),
  FULLTEXT KEY `tc_kimlik_2` (`tc_kimlik`)
) ENGINE=MyISAM DEFAULT CHARSET=latin5;

--
-- Dumping data for table `personel`
--

/*!40000 ALTER TABLE `personel` DISABLE KEYS */;
/*!40000 ALTER TABLE `personel` ENABLE KEYS */;


--
-- Definition of trigger `personel1`
--

DROP TRIGGER /*!50030 IF EXISTS */ `personel1`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `personel1` BEFORE INSERT ON `personel` FOR EACH ROW BEGIN
SET NEW.tarih = NOW();

IF (NEW.dogum_tarihi = '1930-1-1') THEN
SET NEW.dogum_tarihi = NULL;
END IF;

IF (NEW.mail = '') THEN
SET NEW.mail = NULL;
END IF;

IF (NEW.dogum_yeri = '') THEN
SET NEW.dogum_yeri = NULL;
END IF;

IF (NEW.ana_adi = '') THEN
SET NEW.ana_adi = NULL;
END IF;

IF (NEW.baba_adi = '') THEN
SET NEW.baba_adi = NULL;
END IF;

IF (NEW.ssk_no = '') THEN
SET NEW.ssk_no = NULL;
END IF;

IF (NEW.ehliyet = '') THEN
SET NEW.ehliyet = NULL;
END IF;

IF (NEW.adres = '') THEN
SET NEW.adres = NULL;
END IF;

IF (NEW.notlar = '') THEN
SET NEW.notlar = NULL;
END IF;

END $$

DELIMITER ;

--
-- Definition of trigger `personel2`
--

DROP TRIGGER /*!50030 IF EXISTS */ `personel2`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `personel2` BEFORE UPDATE ON `personel` FOR EACH ROW BEGIN

IF (OLD.dogum_tarihi = '1930-1-1') THEN
SET NEW.dogum_tarihi = NULL;
END IF;

IF (OLD.mail = '') THEN
SET NEW.mail = NULL;
END IF;

IF (OLD.dogum_yeri = '') THEN
SET NEW.dogum_yeri = NULL;
END IF;

IF (OLD.ana_adi = '') THEN
SET NEW.ana_adi = NULL;
END IF;

IF (OLD.baba_adi = '') THEN
SET NEW.baba_adi = NULL;
END IF;

IF (OLD.ssk_no = '') THEN
SET NEW.ssk_no = NULL;
END IF;

IF (OLD.ehliyet = '') THEN
SET NEW.ehliyet = NULL;
END IF;

IF (OLD.adres = '') THEN
SET NEW.adres = NULL;
END IF;

IF (OLD.notlar = '') THEN
SET NEW.notlar = NULL;
END IF;

END $$

DELIMITER ;

--
-- Definition of table `proje`
--

DROP TABLE IF EXISTS `proje`;
CREATE TABLE `proje` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bolge_id` int(11) NOT NULL DEFAULT '0',
  `firma_id` int(11) NOT NULL DEFAULT '0',
  `yonetici_id` int(11) NOT NULL DEFAULT '0',
  `baslangic` date NOT NULL DEFAULT '0000-00-00',
  `bitis` date DEFAULT NULL,
  `adi` varchar(500) NOT NULL DEFAULT '',
  `sorumlu` varchar(150) DEFAULT NULL,
  `tel` varchar(13) NOT NULL DEFAULT '000 000 00 00',
  `adres` varchar(150) DEFAULT NULL,
  `notlar` longtext,
  `admin_id` int(11) NOT NULL DEFAULT '0',
  `tarih` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `onay` int(1) NOT NULL DEFAULT '1' COMMENT '0 ise onaysız, 1 ise onaylı',
  PRIMARY KEY (`id`),
  KEY `id` (`id`),
  KEY `tarih` (`tarih`),
  KEY `onay` (`onay`),
  KEY `admin_id` (`admin_id`),
  KEY `bolge_id` (`bolge_id`),
  KEY `firma_id` (`firma_id`),
  KEY `yonetici_id` (`yonetici_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin5;

--
-- Dumping data for table `proje`
--

/*!40000 ALTER TABLE `proje` DISABLE KEYS */;
/*!40000 ALTER TABLE `proje` ENABLE KEYS */;


--
-- Definition of trigger `proje1`
--

DROP TRIGGER /*!50030 IF EXISTS */ `proje1`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `proje1` BEFORE INSERT ON `proje` FOR EACH ROW BEGIN
SET NEW.tarih = NOW();

IF (NEW.bitis = '1930-1-1') THEN
SET NEW.bitis = NULL;
END IF;

IF (NEW.adres = '') THEN
SET NEW.adres = NULL;
END IF;

IF (NEW.notlar = '') THEN
SET NEW.notlar = NULL;
END IF;

END $$

DELIMITER ;

--
-- Definition of trigger `proje2`
--

DROP TRIGGER /*!50030 IF EXISTS */ `proje2`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `proje2` BEFORE UPDATE ON `proje` FOR EACH ROW BEGIN

IF (OLD.baslangic = '1930-1-1') THEN
SET NEW.baslangic = CURDATE();
END IF;

IF (OLD.bitis = '1930-1-1') THEN
SET NEW.bitis = NULL;
END IF;

IF (OLD.adres = '') THEN
SET NEW.adres = NULL;
END IF;

IF (OLD.notlar = '') THEN
SET NEW.notlar = NULL;
END IF;

END $$

DELIMITER ;

--
-- Definition of table `proje_personel`
--

DROP TABLE IF EXISTS `proje_personel`;
CREATE TABLE `proje_personel` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bolge_id` int(11) NOT NULL DEFAULT '0',
  `firma_id` int(11) NOT NULL DEFAULT '0',
  `proje_id` int(11) NOT NULL DEFAULT '0',
  `personel_id` int(11) NOT NULL DEFAULT '0',
  `tamam` int(1) NOT NULL DEFAULT '0',
  `admin_id` int(11) NOT NULL DEFAULT '0',
  `tarih` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`id`),
  KEY `id` (`id`),
  KEY `tarih` (`tarih`),
  KEY `admin_id` (`admin_id`),
  KEY `proje_id` (`proje_id`),
  KEY `tamam` (`tamam`),
  KEY `urun_id` (`personel_id`),
  KEY `firma_id` (`firma_id`),
  KEY `bolge_id` (`bolge_id`),
  KEY `personel_id` (`personel_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin5 ROW_FORMAT=DYNAMIC;

--
-- Dumping data for table `proje_personel`
--

/*!40000 ALTER TABLE `proje_personel` DISABLE KEYS */;
/*!40000 ALTER TABLE `proje_personel` ENABLE KEYS */;


--
-- Definition of trigger `proje_personel`
--

DROP TRIGGER /*!50030 IF EXISTS */ `proje_personel`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `proje_personel` BEFORE INSERT ON `proje_personel` FOR EACH ROW BEGIN
SET NEW.tarih = NOW();
END $$

DELIMITER ;

--
-- Definition of table `proje_personel_urun`
--

DROP TABLE IF EXISTS `proje_personel_urun`;
CREATE TABLE `proje_personel_urun` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bolge_id` int(11) NOT NULL DEFAULT '0',
  `firma_id` int(11) NOT NULL DEFAULT '0',
  `proje_id` int(11) NOT NULL DEFAULT '0',
  `personel_id` int(11) NOT NULL DEFAULT '0',
  `urun_id` int(11) NOT NULL DEFAULT '0',
  `durum` int(1) NOT NULL DEFAULT '1' COMMENT '0 ise eski, 1 ise yeni',
  `adet` int(11) NOT NULL DEFAULT '1',
  `tamam` int(1) NOT NULL DEFAULT '0',
  `admin_id` int(11) NOT NULL DEFAULT '0',
  `tarih` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`id`),
  KEY `id` (`id`),
  KEY `tarih` (`tarih`),
  KEY `admin_id` (`admin_id`),
  KEY `proje_id` (`proje_id`),
  KEY `tamam` (`tamam`),
  KEY `durum` (`durum`),
  KEY `urun_id` (`urun_id`),
  KEY `firma_id` (`firma_id`),
  KEY `personel_id` (`personel_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin5 ROW_FORMAT=DYNAMIC;

--
-- Dumping data for table `proje_personel_urun`
--

/*!40000 ALTER TABLE `proje_personel_urun` DISABLE KEYS */;
/*!40000 ALTER TABLE `proje_personel_urun` ENABLE KEYS */;


--
-- Definition of trigger `proje_personel_urun1`
--

DROP TRIGGER /*!50030 IF EXISTS */ `proje_personel_urun1`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `proje_personel_urun1` BEFORE INSERT ON `proje_personel_urun` FOR EACH ROW BEGIN
SET NEW.tarih = NOW();

IF (NEW.adet = '') THEN
SET NEW.adet = '1';
END IF;

END $$

DELIMITER ;

--
-- Definition of trigger `proje_personel_urun2`
--

DROP TRIGGER /*!50030 IF EXISTS */ `proje_personel_urun2`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `proje_personel_urun2` BEFORE UPDATE ON `proje_personel_urun` FOR EACH ROW BEGIN
IF (OLD.adet = '') THEN
SET NEW.adet = '1';
END IF;
END $$

DELIMITER ;

--
-- Definition of table `proje_urun`
--

DROP TABLE IF EXISTS `proje_urun`;
CREATE TABLE `proje_urun` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `bolge_id` int(11) NOT NULL DEFAULT '0',
  `firma_id` int(11) NOT NULL DEFAULT '0',
  `proje_id` int(11) NOT NULL DEFAULT '0',
  `urun_id` int(11) NOT NULL DEFAULT '0',
  `durum` int(1) NOT NULL DEFAULT '1' COMMENT '0 ise eski, 1 ise yeni',
  `adet` int(11) NOT NULL DEFAULT '1',
  `tamam` int(1) NOT NULL DEFAULT '0',
  `admin_id` int(11) NOT NULL DEFAULT '0',
  `tarih` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  PRIMARY KEY (`id`),
  KEY `id` (`id`),
  KEY `tarih` (`tarih`),
  KEY `admin_id` (`admin_id`),
  KEY `proje_id` (`proje_id`),
  KEY `tamam` (`tamam`),
  KEY `durum` (`durum`),
  KEY `urun_id` (`urun_id`),
  KEY `firma_id` (`firma_id`)
) ENGINE=MyISAM DEFAULT CHARSET=latin5 ROW_FORMAT=DYNAMIC;

--
-- Dumping data for table `proje_urun`
--

/*!40000 ALTER TABLE `proje_urun` DISABLE KEYS */;
/*!40000 ALTER TABLE `proje_urun` ENABLE KEYS */;


--
-- Definition of trigger `proje_urun1`
--

DROP TRIGGER /*!50030 IF EXISTS */ `proje_urun1`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `proje_urun1` BEFORE INSERT ON `proje_urun` FOR EACH ROW BEGIN
SET NEW.tarih = NOW();

IF (NEW.adet = '') THEN
SET NEW.adet = '1';
END IF;

END $$

DELIMITER ;

--
-- Definition of trigger `proje_urun2`
--

DROP TRIGGER /*!50030 IF EXISTS */ `proje_urun2`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `proje_urun2` BEFORE UPDATE ON `proje_urun` FOR EACH ROW BEGIN
IF (OLD.adet = '') THEN
SET NEW.adet = '1';
END IF;
END $$

DELIMITER ;

--
-- Definition of table `urun`
--

DROP TABLE IF EXISTS `urun`;
CREATE TABLE `urun` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `urun` varchar(255) DEFAULT NULL,
  `kod` varchar(255) DEFAULT NULL,
  `admin_id` int(11) NOT NULL DEFAULT '0',
  `tarih` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `onay` int(1) NOT NULL DEFAULT '1' COMMENT '0 ise onaysız, 1 ise onaylı',
  PRIMARY KEY (`id`),
  KEY `id` (`id`),
  KEY `tarih` (`tarih`),
  KEY `onay` (`onay`),
  KEY `admin_id` (`admin_id`),
  FULLTEXT KEY `urun` (`urun`)
) ENGINE=MyISAM DEFAULT CHARSET=latin5;

--
-- Dumping data for table `urun`
--

/*!40000 ALTER TABLE `urun` DISABLE KEYS */;
/*!40000 ALTER TABLE `urun` ENABLE KEYS */;


--
-- Definition of trigger `urun1`
--

DROP TRIGGER /*!50030 IF EXISTS */ `urun1`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `urun1` BEFORE INSERT ON `urun` FOR EACH ROW BEGIN
SET NEW.tarih = NOW();

IF (NEW.kod = '') THEN
SET NEW.kod = NULL;
END IF;

END $$

DELIMITER ;

--
-- Definition of trigger `urun2`
--

DROP TRIGGER /*!50030 IF EXISTS */ `urun2`;

DELIMITER $$

CREATE DEFINER = `root`@`localhost` TRIGGER `urun2` BEFORE UPDATE ON `urun` FOR EACH ROW BEGIN

IF (NEW.kod = '') THEN
SET NEW.kod = NULL;
END IF;

END $$

DELIMITER ;

--
-- Definition of view `depo_urunler`
--

DROP TABLE IF EXISTS `depo_urunler`;
DROP VIEW IF EXISTS `depo_urunler`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `depo_urunler` AS select `depo`.`id` AS `id`,`depo`.`urun_id` AS `urun_id`,cast(ifnull(if(((select `urun`.`kod` from `urun` USE INDEX (`id`) where (`urun`.`id` = `depo`.`urun_id`)) <> ''),concat((select `urun`.`urun` from `urun` USE INDEX (`id`) where (`urun`.`id` = `depo`.`urun_id`)),' - ',(select `urun`.`kod` from `urun` USE INDEX (`id`) where (`urun`.`id` = `depo`.`urun_id`))),NULL),(select `urun`.`urun` from `urun` USE INDEX (`id`) where (`urun`.`id` = `depo`.`urun_id`))) as char(510) charset latin5) AS `urun`,`depo`.`durum` AS `durum` from `depo` USE INDEX (`tip`) USE INDEX (`onay`) where ((`depo`.`tip` = '0') and (`depo`.`onay` = '1'));

--
-- Definition of view `depo_urunler_say`
--

DROP TABLE IF EXISTS `depo_urunler_say`;
DROP VIEW IF EXISTS `depo_urunler_say`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `depo_urunler_say` AS select `depo`.`bolge_id` AS `bolge_id`,`depo`.`firma_id` AS `firma_id`,`depo`.`urun_id` AS `urun_id`,cast(ifnull(if(((select `urun`.`kod` from `urun` USE INDEX (`ID`) where (`urun`.`id` = `depo`.`urun_id`)) <> ''),concat((select `urun`.`urun` from `urun` USE INDEX (`ID`) where (`urun`.`id` = `depo`.`urun_id`)),' - ',(select `urun`.`kod` from `urun` USE INDEX (`ID`) where (`urun`.`id` = `depo`.`urun_id`))),NULL),(select `urun`.`urun` from `urun` USE INDEX (`ID`) where (`urun`.`id` = `depo`.`urun_id`))) as char(510) charset latin5) AS `urun`,`depo`.`durum` AS `durum`,`depo`.`tip` AS `tip`,count(`depo`.`urun_id`) AS `adet` from `depo` USE INDEX (`durum`) USE INDEX (`urun_id`) USE INDEX (`firma_id`) USE INDEX (`tip`) group by `depo`.`durum`,`depo`.`urun_id`,`depo`.`firma_id`,`depo`.`tip`;

--
-- Definition of view `depo_urunler_toplamlar`
--

DROP TABLE IF EXISTS `depo_urunler_toplamlar`;
DROP VIEW IF EXISTS `depo_urunler_toplamlar`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `depo_urunler_toplamlar` AS select `depo`.`bolge_id` AS `bolge_id`,`depo`.`firma_id` AS `firma_id`,`depo`.`urun_id` AS `urun_id`,cast(ifnull(if(((select `urun`.`kod` from `urun` USE INDEX (`ID`) where (`urun`.`id` = `depo`.`urun_id`)) <> ''),concat((select `urun`.`urun` from `urun` USE INDEX (`ID`) where (`urun`.`id` = `depo`.`urun_id`)),' - ',(select `urun`.`kod` from `urun` USE INDEX (`ID`) where (`urun`.`id` = `depo`.`urun_id`))),NULL),(select `urun`.`urun` from `urun` USE INDEX (`ID`) where (`urun`.`id` = `depo`.`urun_id`))) as char(510) charset latin5) AS `urun`,`depo`.`durum` AS `durum`,sum(`depo`.`adet`) AS `adet` from `depo` USE INDEX (`bolge_id`) USE INDEX (`firma_id`) USE INDEX (`urun_id`) USE INDEX (`durum`) group by `depo`.`urun_id`,`depo`.`durum` union select `depo`.`bolge_id` AS `bolge_id`,`depo`.`firma_id` AS `firma_id`,`depo`.`urun_id` AS `urun_id`,cast(ifnull(if(((select `urun`.`kod` from `urun` USE INDEX (`ID`) where (`urun`.`id` = `depo`.`urun_id`)) <> ''),concat((select `urun`.`urun` from `urun` USE INDEX (`ID`) where (`urun`.`id` = `depo`.`urun_id`)),' - ',(select `urun`.`kod` from `urun` USE INDEX (`ID`) where (`urun`.`id` = `depo`.`urun_id`))),NULL),(select `urun`.`urun` from `urun` USE INDEX (`ID`) where (`urun`.`id` = `depo`.`urun_id`))) as char(510) charset latin5) AS `urun`,`depo`.`durum` AS `durum`,sum((`depo`.`adet` * -(1))) AS `adet` from `depo` USE INDEX (`bolge_id`) USE INDEX (`firma_id`) USE INDEX (`urun_id`) USE INDEX (`durum`) where ((`depo`.`bolge_id` <> '0') and (`depo`.`firma_id` <> '0')) group by `depo`.`urun_id`,`depo`.`durum`;



/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
