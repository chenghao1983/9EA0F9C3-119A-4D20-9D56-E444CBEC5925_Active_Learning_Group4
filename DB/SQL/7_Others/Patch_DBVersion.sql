
DECLARE @DBVersion varchar(255)
SET @DBVersion='Schema revision 1.1'
update DBVersion set DBVersion = @DBVersion, CreateDT = getdate();