using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.lib
{
    public enum MenuStates:byte
    {
        CarsAll=1,
        CarById,
        CarAdd,
        CarEdit,
        CarRemove,

        ModelsAll,
        ModelById,
        ModelAdd,
        ModelEdit,
        ModelRemove,

        BrandsAll,
        BrandById,
        BrandAdd,
        BrandEdit,
        BrandRemove,

        Save,
        Exit
    }
}
