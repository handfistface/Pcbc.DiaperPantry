DROP PROCEDURE IF EXISTS `Pcbc.DiaperPantry`.Create_Child;

DELIMITER $$
$$
CREATE PROCEDURE `Pcbc.DiaperPantry`.Create_Child(
	name varchar(100),
	diaperStyle varchar(100),
	diaperSize varchar(100),
	birthday datetime,
	parentId int
)
BEGIN
	INSERT INTO `Pcbc.DiaperPantry`.Child
(Name, DiaperStyle, DiaperSize, Birthday, ParentId)
VALUES(name, diaperStyle, diaperSize, birthday, parentId);

END$$
DELIMITER ;
