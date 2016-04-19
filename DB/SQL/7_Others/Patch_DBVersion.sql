
DECLARE @DBVersion varchar(255)
SET @DBVersion='Schema revision 1.4'
update DBVersion set DBVersion = @DBVersion, CreateDT = '2016-04-19';