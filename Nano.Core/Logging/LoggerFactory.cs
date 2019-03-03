using System;
using System.Collections.Generic;
using System.Text;
using CLogger.Core;
using CLogger.Logging;
using CLogger.Writers;

namespace Nano.Core.Logging
{
    public class LoggerFactory
    {
        public static Logger CreateSingleFileLogger(string path, ClogTemplate template = null)
        {
            ClogTemplate ct = template;
            if (ct == null)
            {
                ct = ClogTemplate.Builder.Build();
            }
            return new Logger(ct, new CLogSingleFileWriter(path, false), false);
        }

        public static Logger CreateRollingFileLogger(string directory, string filenamePrefix, int maxKeepFiles)
        {
            return new Logger(
                ClogTemplate.Builder.Build(),
                new CLogRollingFileWriter(directory)
                    .WithLock(false)
                    .SetFileNamePrefix(filenamePrefix)
                    .SetMaxKeepFiles(maxKeepFiles)
                    .SetStrategy(DateTimeStrategy.DATETIME_DAY),
                false);
        }
        
    }
}
