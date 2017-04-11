using System.Threading.Tasks;

namespace Tracker_History.Helpers {
   /// <summary>
   /// A do nothing task that can be returned
   /// from functions that require task results
   /// when there's nothing to do.
   /// 
   /// This essentially returns a completed task
   /// with an empty value structure result.
   /// </summary>
   public class EmptyTask {
      public static Task Start() {
         var taskSource = new TaskCompletionSource<AsyncVoid>();
         taskSource.SetResult(default(AsyncVoid));
         return taskSource.Task as Task;
      }

      private struct AsyncVoid {
      }
   }
}