﻿using RVA_Projekat.Model;

namespace RVA_Projekat.Interface
{
    public interface IBrutoHonorarRepository:IRepository<BrutoHonorar>
    {
        BrutoHonorar Edit(BrutoHonorar bh);
    }
}
