using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetterTravel.Common.Utils
{
    public static class TaskHelper
    {
        public static async Task WhenAll(this IEnumerable<Task> tasks) =>
            await Task.WhenAll(tasks);
    }
}