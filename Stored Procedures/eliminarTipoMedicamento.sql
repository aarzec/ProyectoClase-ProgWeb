create procedure uspEliminarTipoMedicamento
@idTipoMedicamento int
as
begin

update TipoMedicamento
set BHABILITADO=0
where IIDTIPOMEDICAMENTO = @idTipoMedicamento

end