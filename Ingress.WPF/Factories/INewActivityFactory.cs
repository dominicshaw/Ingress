﻿using Ingress.Data.Models;
using Ingress.WPF.ViewModels.Data;

namespace Ingress.WPF.Factories
{
    public delegate void NewActivityEventHandler(ActivityViewModel activity);

    public interface INewActivityFactory
    {
        event NewActivityEventHandler NewActivity;
        ActivityViewModel Load(Activity activity);
    }
}