using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace eMailSender_v1._2.Data
{
    class Server
    {
        private int _Port;

        public string Address { get; set; }
        
        public int Port
        {
            get => _Port;
            set
            {
                if (value < 0 || value >= 65335)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "Номер порта должен быть в диапазоне от 0 до 65335");
                _Port = value;
            }
        }

        

    }
}
