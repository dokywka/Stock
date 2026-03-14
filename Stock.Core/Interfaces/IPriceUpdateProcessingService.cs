using System;
using System.Collections.Generic;
using System.Text;

namespace StockApp.Core.Interfaces
{
    public interface IPriceUpdateProcessingService
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}
