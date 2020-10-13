using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Core.Exceptions.Helpers
{
    public static class ErrorHandlerHelper
    {
        private const string Line = "-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-";
        
        public static void Handle(
            Exception exception,
            string additionalMessage = null,
            [CallerMemberName] string memberName = null,
            [CallerLineNumber] int lineNumber = -1,
            [CallerFilePath] string filepath = null)
        {
            if (Debugger.IsAttached)
            {
                var builder = new StringBuilder();

                AppendMessage(builder, "Own message", additionalMessage);
                AppendMessage(builder, "File path", filepath);
                AppendMessage(builder, "Line", lineNumber.ToString());
                AppendMessage(builder, "Member name", memberName);
                AppendExceptionData(builder, exception);
                
                Console.WriteLine(builder.ToString());
                Debugger.Break();
            }
        }

        private static void AppendExceptionData(StringBuilder builder, Exception exception)
        {
            if (exception != null)
            {
                AppendMessage(builder, "Exception type", exception.GetType().FullName);
                AppendMessage(builder, "Message", exception.Message);
                builder.AppendLine(exception.StackTrace);
                builder.AppendLine(Line);
                AppendMessage(builder, "Source", exception.Source);

                if (exception.InnerException != null)
                {
                    builder.AppendLine("InnerException");
                    AppendExceptionData(builder, exception.InnerException);
                }
            }
        }

        private static void AppendMessage(StringBuilder builder, string key, string message)
        {
            builder.AppendLine(key + ": " + message);
            builder.AppendLine(Line);
        }
    }
}