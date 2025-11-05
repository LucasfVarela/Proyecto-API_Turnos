namespace API_CoreBusiness.Entities
{
    public class Turno
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Observacion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public Turno() { }

        public Turno(int idUsuario, string observacion)
        {
            IdUsuario = idUsuario;
            Observacion = observacion;
            FechaCreacion = DateTime.Now;
        }
    }
}