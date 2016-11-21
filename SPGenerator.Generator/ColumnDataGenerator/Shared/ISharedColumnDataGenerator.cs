using SPGenerator.Generator.ColumnDataGenerator.Boolean;
using SPGenerator.Generator.ColumnDataGenerator.Choice;
using SPGenerator.Generator.ColumnDataGenerator.Currency;
using SPGenerator.Generator.ColumnDataGenerator.DateTime;
using SPGenerator.Generator.ColumnDataGenerator.MultilineText;
using SPGenerator.Generator.ColumnDataGenerator.Number;
using SPGenerator.Generator.ColumnDataGenerator.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPGenerator.Generator.ColumnDataGenerator.Shared
{
    public interface ISharedDataGenerator: IBooleanDataGenerator, IChoiceDataGenerator, IDateTimeDataGenerator,
        IMultilineTextDataGenerator, INumberDataGenerator, ITextDataGenerator, ICurrencyDataGenerator
    {
    }
}