using Microsoft.Extensions.Options;
using SMS_System.Model.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS_System.Services
{
    public class BaseRepository
    {
        public readonly IOptions<DataConfig> _dataConfig;

        BaseRepository(IOptions<DataConfig> dataConfig) { _dataConfig = dataConfig; }
    }
}
