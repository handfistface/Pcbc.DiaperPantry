DROP PROCEDURE IF EXISTS `Pcbc.DiaperPantry`.Update_Child;

DELIMITER $$
$$
CREATE PROCEDURE `Pcbc.DiaperPantry`.Update_Child(
	childId int,
	name varchar(100),
	diaperSize varchar(100),
	diaperStyle varchar(100),
	birthday datetime,
	parentId int
	)
BEGIN
	UPDATE `Pcbc.DiaperPantry`.Child
	SET 
		Name = name, 
		DiaperSize = diaperSize,
		DiaperStyle = diaperStyle,
		Birthday = birthday,
		ParentId = parentId
	WHERE 
		Id = childId;
END$$
DELIMITER ;
