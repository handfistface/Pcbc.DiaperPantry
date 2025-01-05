DROP PROCEDURE IF EXISTS `Pcbc.DiaperPantry`.Exists_Child;

DELIMITER $$
$$
CREATE PROCEDURE `Pcbc.DiaperPantry`.Exists_Child(
	firstLastName VARCHAR(250),
	birth DATETIME,
	parent INT
)
BEGIN
	
	SELECT EXISTS (
		SELECT 1 FROM `Pcbc.DiaperPantry`.Child 
		WHERE 
		Name = firstLastName and 
		Birthday = birth and
		parentId = parent
	) as DoesChildExist;
	
END$$
DELIMITER ;
