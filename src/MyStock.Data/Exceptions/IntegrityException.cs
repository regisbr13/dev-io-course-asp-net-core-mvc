using Microsoft.EntityFrameworkCore;

namespace MyStock.Data.Exceptions
{
    public class IntegrityException : DbUpdateException
    {
        public IntegrityException(string msg) : base(msg)
        {
        }
    }
}
