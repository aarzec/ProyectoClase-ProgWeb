CREATE PROCEDURE uspGuardarTipoMedicamento
@idTipoMedicamento int,
@nombre varchar(100),
@descripcion varchar(300)
as
begin

if @idTipoMedicamento=0
--insert
	insert into TipoMedicamento(NOMBRE, DESCRIPCION, BHABILITADO)
	values(@nombre,@descripcion,1)
else
--update
	update TipoMedicamento
	set
		NOMBRE = @nombre,
		DESCRIPCION = @descripcion
	where IIDTIPOMEDICAMENTO = @idTipoMedicamento
end;