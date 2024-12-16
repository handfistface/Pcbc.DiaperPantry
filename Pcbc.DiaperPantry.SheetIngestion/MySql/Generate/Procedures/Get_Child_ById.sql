CREATE PROCEDURE `Pcbc.DiaperPantry`.Get_Child_ById(
	childId int
)
BEGIN
	select * from `Pcbc.DiaperPantry`.Child
	where id = childId;
END