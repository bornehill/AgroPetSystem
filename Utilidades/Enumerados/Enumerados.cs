
namespace Utilidades.Enumerados
{
    /// <summary>
    /// Enumerados que definen los tipos de intervalos de tiempo
    /// </summary>
    /// 
    public enum DateInterval
    {
        Day,
        DayOfYear,
        Hour,
        Minute,
        Month,
        Quarter,
        Second,
        Weekday,
        WeekOfYear,
        Year
    }

    /// <summary>
    /// Enumerados que define el  valor devuelto(cadena) de un bool
    /// </summary>
    public enum StringStatus
    {
        Enable,
        Disable,
        Activo,
        Inactivo
    }

    /// <summary>
    /// Enumerados que definen los dias de la semana en idioma españos
    /// </summary>
    public enum Dias
    {
        Lunes = 1,
        Martes,
        Miércoles,
        Jueves,
        Viernes,
        Sábado,
        Domingo,
    }

    /// <summary>
    /// Enumerados que definen los meses del año(idioma Español)
    /// </summary>
    public enum Meses
    {
        Enero = 1,
        Febrero,
        Marzo,
        Abril,
        Mayo,
        Junio,
        Julio,
        Agosto,
        Septiembre,
        October,
        Noviembre,
        Diciembre,
    }

    /// <summary>
    /// Enumerados que definen los dias de la semana(idioma Ingles)
    /// </summary>
    public enum Days
    {
        Monday = 1,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday,
    }

    /// <summary>
    /// Enumerados que definen los meses del año(idioma Ingle)
    /// </summary>
    public enum Months
    {
        January = 1,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December,
    }

    public enum Elementos
    {
        Articulo=1,
        Servicio=3,
        Paquete=2,
    }

    public enum Individuos
    {
        Cliente = 1,
        Proveedor=3,
    }

    public enum TipoDirecciones
    {
        Fiscal = 1,
        Envio = 2,
    }

    public enum Catalogos
    {
        Clientes = 1,
        Proveedores = 2,
        Documentos = 3,
        Empresas = 4,
    }

    public enum PunctuationMarks
    {
        Period = '.'
        ,Comma = ','
        ,Semicolon = ';'
        ,Ccolon = ':'

        ,ExclamationMarkOpen = '¡'
        ,ExclamationMarkClose = '!'

        ,QuestionMarkOpen = '¿'
        ,QuestionMarkClose = '?'

        ,Slash = '/'
        ,AtSign = '@'
        ,Asterisk = '*'
        ,Ampersand = '&'
        ,NumberSign = '#'

        ,Apostrophe = '‘'

        ,QuotationMarksSingle = '\''
        ,QuotationMarksDouble = '\"'

        ,QuotationMarksSingleOpen = '‘'
        ,QuotationMarksSingleClose = '’'
        ,QuotationMarksDoubleOpen = '“'
        ,QuotationMarksDoubleClose = '”'

        ,Dash = '—'
        ,Underscore = '_'

        ,Pipe = '|'
        ,Tilde = '~'
        ,AcuteAccent = '´'

        ,ParenthesesOpen = '('
        ,ParenthesesClose = ')'
        ,BracketsOpen = '['
        ,Bracketsclose = ']'
        ,CurlyBracketsOpen = '{'
        ,CurlyBracketsClose = '}'

        ,Backslash = '\\'
        ,InequalitySignsOpen = '<'
        ,InequalitySignsClose = '>'
    }

    public enum OperationSigns
    {
       Plus = '+'
       ,Minus = '-'
       ,Multiplied = '*'
       ,Divided = '/'
       ,Equals = '='
       ,PerCent ='%'
       ,Exponent = '^'
    }

    public enum SQLInjectionMarks
    {
        MoneyMark = '$'
        ,ExclamationMark = '!'
        ,QuestionMark = '?'
        ,Slash = '/'
        ,Asterisk = '*'
        ,Ampersand = '&'

        ,QuotationMarksSingle = '\''
        ,QuotationMarksDouble = '\"'

        ,Pipe = '|'

        ,BracketsOpen = '['
        ,Bracketsclose = ']'
        ,CurlyBracketsOpen = '{'
        ,CurlyBracketsClose = '}'

        ,Backslash = '\\'
        ,InequalitySignsOpen = '<'
        ,InequalitySignsClose = '>'

        #region OperationSigns
        ,Plus = '+'
        ,Minus = '-'
        ,Equals = '='
        ,PerCent = '%'
        ,Exponent = '^'
        #endregion
    }
}