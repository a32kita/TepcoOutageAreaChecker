using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TepcoOutageSharp;

namespace TepcoOutageAreaChecker
{
    public class ExecutionResult
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }

        public TepcoAreaOutageInfo? Result { get; set; }


        public DateTimeOffset RetrievedAt { get; set; }
    }
}
