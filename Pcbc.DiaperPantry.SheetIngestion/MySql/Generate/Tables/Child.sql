CREATE TABLE `Pcbc.DiaperPantry`.Child (
	Name varchar(100) NOT NULL,
	DiaperStyle varchar(100) NOT NULL,
	DiaperSize varchar(100) NOT NULL,
	Birthday DATETIME NULL,
	ParentId INT NOT NULL DEFAULT 0,
	Id INT auto_increment NOT NULL,
	CONSTRAINT Child_PK PRIMARY KEY (Id)
)
ENGINE=InnoDB
DEFAULT CHARSET=utf8mb4
COLLATE=utf8mb4_0900_ai_ci;