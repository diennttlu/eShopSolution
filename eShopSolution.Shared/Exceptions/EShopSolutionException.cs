using System;

namespace eShopSolution.Shared.Exceptions
{
    public class EShopSolutionException : Exception
    {
        public EShopSolutionException()
        {

        }

        public EShopSolutionException(string message) 
            : base(message) 
        {

        }

        public EShopSolutionException(string message, Exception inner) 
            : base(message, inner)
        {

        }
    }
}
