namespace API.Modelos
{
    public class DatosSensor
    {
         public int ID { get; set; }
        public float Temperatura { get; set; }
        public float Distance1 { get; set; }
        public float Distance2 { get; set; }
        public float TDSValue { get; set; }
        public float PHValue { get; set; }
        public DateTime FechaHora { get; set; }
    }
    
}
