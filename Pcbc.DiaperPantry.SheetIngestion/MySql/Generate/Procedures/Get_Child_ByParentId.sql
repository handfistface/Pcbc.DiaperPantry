DROP PROCEDURE IF EXISTS `Pcbc.DiaperPantry`.Get_Child_ByParentId;

DELIMITER $$
$$
CREATE PROCEDURE `Pcbc.DiaperPantry`.Get_Child_ByParentId(
	parent int
	)
BEGIN
	select * from `Pcbc.DiaperPantry`.Child
	where parentId = parent;
END$$
DELIMITER ;
