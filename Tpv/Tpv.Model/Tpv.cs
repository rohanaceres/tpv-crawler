namespace Tpv.Model
{
    /// <summary>
    /// Representa o conjunto de dados sobre o TPV.
    /// </summary>
    public class Tpv
    {
        /// <summary>
        /// TPV total em toda a história da Stone.
        /// </summary>
        double Total { get; set; }
        /// <summary>
        /// TPV total no ultimo ano.
        /// </summary>
        double LastYear { get; set; }
        /// <summary>
        /// TPV total somente esse ano.
        /// </summary>
        double ThisYear { get; set; }
        /// <summary>
        /// TPV dos ultimos 6 meses.
        /// </summary>
        double Last6Months { get; set; }
        /// <summary>
        /// TPV to mês anterior.
        /// </summary>
        double LastMonth { get; set; }
        /// <summary>
        /// TPV desse mês.
        /// </summary>
        double ThisMonth { get; set; }
        /// <summary>
        /// TPV da ultima semana.
        /// </summary>
        double Last7Days { get; set; }
        /// <summary>
        /// TPV de ontem.
        /// </summary>
        double Yesterday { get; set; }
        /// <summary>
        /// TPV de hoje.
        /// </summary>
        double Today { get; set; }
        /// <summary>
        /// Meta do mês.
        /// </summary>
        double Goal { get; set; }
    }
}
