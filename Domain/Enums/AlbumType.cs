using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    [Flags]
    public enum AlbumType
    {
        album = 1,
        single = 2,
        appears_on = 4,
        compilation = 8,
    }
}
