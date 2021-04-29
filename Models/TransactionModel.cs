using System;

namespace jwtapi_app.Models
{
    public class TransactionProd
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public DateTime Time { get; set; }
    }
}