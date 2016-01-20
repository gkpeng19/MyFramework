﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityLibrary.Entities
{
    public enum EnumDPanelType
    {
        Distination = 1,
        MemberBenefit = 2,
        MemberService = 3,
        NewCheaper = 4
    }

    public enum EnumUserType
    {
        Normal = 1,
        Vip = 2
    }

    public enum EnumArticleCategory
    {
        TravelGuide = 1,
        ContactUs = 2,
        SilderImg = 3
    }

    public enum EnumOrderState
    {
        Wainting = 0,
        Booked = 1,
        UnBooked = 2,
        Payed = 3,
        Canceled = 4,
        Deleted = 5
    }
}