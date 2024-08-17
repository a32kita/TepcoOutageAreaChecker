using System.Text.Json;
using TepcoOutageSharp;

namespace TepcoOutageAreaChecker
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var asmPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var asmDir = Path.GetDirectoryName(asmPath);
            var asmFileName = Path.GetFileName(asmPath);

            var executionResult = new ExecutionResult();

            if (args.Length == 0)
            {
                executionResult.Success = false;
                executionResult.ErrorMessage = $"[Command] usage: {asmFileName} <AreaCode>";
            }
            else
            {
                var tepcoOISv = new TepcoOutageInfoService();
                await MainProc(tepcoOISv, args[0], executionResult);
            }

            using (var outputStream = Console.OpenStandardOutput())
                await JsonSerializer.SerializeAsync(outputStream, executionResult, new JsonSerializerOptions() { WriteIndented = true });
        }

        static async Task MainProc(TepcoOutageInfoService tepcoOISv, string areaCode, ExecutionResult executionResult)
        {
            try
            {
                executionResult.Result = await tepcoOISv.GetAreaOutageInfoAsync(new TepcoAreaCode(areaCode));
                executionResult.Success = true;
            }
            catch (Exception ex)
            {
                executionResult.Success = false;
                executionResult.ErrorMessage = $"[Exeption] {ex.GetType().Name}: {ex.Message}";
            }
        }
    }
}
