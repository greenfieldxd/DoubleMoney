using System;
using System.Collections;
using System.Globalization;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Source.Scripts.Extensions
{
    public static class OtherExtensions
    {
        public static string FormatNumberWithCommas(int value)
        {
            return string.Format(new CultureInfo("en-US"), "{0:N0}", value);
        }
    }
}