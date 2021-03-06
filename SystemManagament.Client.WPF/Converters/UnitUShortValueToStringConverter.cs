﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using SystemManagament.Client.WPF.WorkstationMonitorService;

namespace SystemManagament.Client.WPF.Converters
{
    public sealed class UnitUShortValueToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var unitValueObject = (UnitUShortValue)value;

            if (unitValueObject.Value == null)
            {
                return "No data";
            }

            var objectValue = unitValueObject.Value.ToString();
            var unit = unitValueObject.Unit;

            return objectValue + " " + unit;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
