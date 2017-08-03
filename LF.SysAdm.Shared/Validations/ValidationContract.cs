using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LF.SysAdm.Shared.Validations
{
    public class ValidationContract<T> where T : Notifiable
    {
        T _entity;

        public ValidationContract(T Entity)
        {
            _entity = Entity;
        }

        /// <summary>
        ///  Dada uma string, adicione uma notificação se for nula ou vazia
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsRequired(Expression<Func<T, string>> selector, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (string.IsNullOrEmpty(val))
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} é Requerido." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se seu comprimento for menor que o parâmetro min
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="min">Minimum Length</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> HasMinLenght(Expression<Func<T, string>> selector, int min, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (!string.IsNullOrEmpty(val) && val.Length < min)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ter pelo menos {min} characters." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se seu comprimento for maior que o parâmetro max
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="max">Maximum Length</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> HasMaxLenght(Expression<Func<T, string>> selector, int max, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (!string.IsNullOrEmpty(val) && val.Length > max)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ter  {max} characters." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se seu comprimento for diferente do parâmetro length
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="length">Especific Length</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsFixedLenght(Expression<Func<T, string>> selector, int length, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (!string.IsNullOrEmpty(val) && val.Length != length)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ter  exatamente {length} characters." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se não for um endereço de e-mail válido
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsEmail(Expression<Func<T, string>> selector, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (!Regex.IsMatch(val, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ter  E-mail valido." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se não for um URL válido
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsUrl(Expression<Func<T, string>> selector, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (!Regex.IsMatch(val, @"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$"))
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} deve ter um  URL. valida" : message);

            return this;
        }

        /// <summary>
        /// Dado um int, adicione uma notificação se não for maior do que algum outro valor
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="number">Number to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsGreaterThan(Expression<Func<T, int>> selector, int number, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val < number)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} deve ser maior que {number}." : message);

            return this;
        }

        /// <summary>
        /// Dado um int, adicione uma notificação se não for maior do que algum outro valor
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="number">Number to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsGreaterOrEqualsThan(Expression<Func<T, int>> selector, int number, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val <= number)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} deve ser maior que  {number}." : message);

            return this;
        }

        /// <summary>
        /// Dado (Dado um valor) um decimal, adicione uma notificação se não for maior (Maior) do que algum outro valor
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="number">Number to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsGreaterThan(Expression<Func<T, decimal>> selector, decimal number, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val < number)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name}Deve ser maior que {number}." : message);

            return this;
        }

        /// <summary>
        /// Dado um decimal, adicione uma notificação se não for maior do que algum outro valor
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="number">Number to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsGreaterOrEqualsThan(Expression<Func<T, decimal>> selector, decimal number, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val <= number)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name}Deve ser maior que {number}." : message);

            return this;
        }

        /// <summary>
        ///Dado um double, adicione uma notificação se não for maior que algum outro valor
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="number">Number to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsGreaterThan(Expression<Func<T, double>> selector, double number, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val < number)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name}Deve ser maior que {number}." : message);

            return this;
        }

        /// <summary>
        /// Dado um double, adicione uma notificação se não for maior que algum outro valor
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="number">Number to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsGreaterOrEqualsThan(Expression<Func<T, double>> selector, double number, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val <= number)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name}Deve ser maior que {number}." : message);

            return this;
        }

        /// <summary>
        /// Dada uma data, adicione uma notificação se não for superior a outra data
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="date">Date to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsGreaterThan(Expression<Func<T, DateTime>> selector, DateTime date, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val < date)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name}Deve ser maior que {date.ToString("MM/dd/yyyy")}." : message);

            return this;
        }

        /// <summary>
        ///Dada uma data, adicione uma notificação se não for superior a outra data
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="date">Date to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsGreaterOrEqualsThan(Expression<Func<T, DateTime>> selector, DateTime date, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val <= date)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name}Deve ser maior que {date.ToString("MM/dd/yyyy")}." : message);

            return this;
        }

        /// <summary>
        /// Dado um int, adicione uma notificação se não for inferior a algum outro valor
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="number">Number to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsLowerThan(Expression<Func<T, int>> selector, int number, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val > number)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser menor que {number}." : message);

            return this;
        }

        /// <summary>
        /// Dado um int, adicione uma notificação se não for inferior a algum outro valor
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="number">Number to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsLowerOrEqualsThan(Expression<Func<T, int>> selector, int number, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val >= number)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser menor que {number}." : message);

            return this;
        }

        /// <summary>
        ///         Dado um decimal, adicione uma notificação se não for inferior a algum outro valor
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="number">Number to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsLowerThan(Expression<Func<T, decimal>> selector, decimal number, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val > number)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser menor que {number}." : message);

            return this;
        }

        /// <summary>
        /// Dado um decimal, adicione uma notificação se não for inferior a algum outro valor
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="number">Number to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsLowerOrEqualsThan(Expression<Func<T, decimal>> selector, decimal number, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val >= number)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser menor que {number}." : message);

            return this;
        }

        /// <summary>
        ///         Dado um double, adicione uma notificação se não for inferior a algum outro valor
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="number">Number to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsLowerThan(Expression<Func<T, double>> selector, double number, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val > number)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser menor que {number}." : message);

            return this;
        }

        /// <summary>
        /// Dado um duplo, adicione uma notificação se não for inferior a algum outro valor
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="number">Number to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsLowerOrEqualsThan(Expression<Func<T, double>> selector, double number, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val >= number)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser menor que {number}." : message);

            return this;
        }

        /// <summary>
        ///Dada uma data, adicione uma notificação se não for inferior a outra data
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="date">Date to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsLowerThan(Expression<Func<T, DateTime>> selector, DateTime date, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val > date)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser menor que {date.ToString("MM/dd/yyyy")}." : message);

            return this;
        }

        /// <summary>
        /// Dada uma data, adicione uma notificação se não for inferior a outra data
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="date">Date to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsLowerOrEqualsThan(Expression<Func<T, DateTime>> selector, DateTime date, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val >= date)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser menor que {date.ToString("MM/dd/yyyy")}." : message);

            return this;
        }

        /// <summary>
        ///  Dado um int, adicione uma notificação se não for entre alguns dois valores
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="a">Lower value</param>
        /// <param name="b">Higher value</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsBetween(Expression<Func<T, int>> selector, int a, int b, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val < a || val > b)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve estar entre {a} and {b}." : message);

            return this;
        }

        /// <summary>
        /// Dado um decimal, adicione uma notificação se não for entre alguns dois valores
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="a">Lower value</param>
        /// <param name="b">Higher value</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsBetween(Expression<Func<T, decimal>> selector, decimal a, decimal b, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val < a || val > b)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve estar entre {a} and {b}." : message);

            return this;
        }

        /// <summary>
        /// Dado um double, adicione uma notificação se não for entre alguns valores
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="a">Lower value</param>
        /// <param name="b">Higher value</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsBetween(Expression<Func<T, double>> selector, double a, double b, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val < a || val > b)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve estar entre {a} and {b}." : message);

            return this;
        }

        /// <summary>
        /// Dada uma data, adicione uma notificação se não for entre alguns dois valores
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="a">Lower value</param>
        /// <param name="b">Higher value</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsBetween(Expression<Func<T, DateTime>> selector, DateTime a, DateTime b, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val < a || val > b)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve estar entre {a.ToString("MM/dd/yyyy")} and {b.ToString("MM/dd/yyyy")}." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se não contiver um texto
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="a">Lower value</param>
        /// <param name="b">Higher value</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> Contains(Expression<Func<T, string>> selector, string text, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (!val.Contains(text))
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser contém{text}." : message);

            return this;
        }

        /// <summary>
        ///         Dado um objeto, adicione uma notificação se não for nulo
        /// </summary>
        /// <param name="obj">Property</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsNull(object obj, string message)
        {
            if (obj != null)
                _entity.AddNotification("", message);

            return this;
        }

        /// <summary>
        /// Dado um objeto, adicione uma notificação se for nulo
        /// </summary>
        /// <param name="obj">Property</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsNotNull(object obj, string message)
        {
            if (obj == null)
                _entity.AddNotification("", message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se não for igual a outra
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="val">Value to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> AreEquals(Expression<Func<T, string>> selector, string text, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (!val.Equals(text, StringComparison.OrdinalIgnoreCase))
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser igual a {text}." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se não for igual a outra
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="val">Value to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> AreEquals(Expression<Func<T, int>> selector, int val, string message = "")
        {
            var data = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (data != val)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser igual a {val}." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se não for igual a outra
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="val">Value to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> AreEquals(Expression<Func<T, decimal>> selector, decimal val, string message = "")
        {
            var data = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (data != val)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser igual a {val}." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se não for igual a outra
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="val">Value to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> AreEquals(Expression<Func<T, double>> selector, double val, string message = "")
        {
            var data = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (data != val)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser igual a {val}." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se não for igual a outra
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="val">Value to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> AreEquals(Expression<Func<T, bool>> selector, bool val, string message = "")
        {
            var data = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (data != val)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser igual a {val}." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se não for igual a outra
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="val">Value to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> AreEquals(Expression<Func<T, DateTime>> selector, DateTime val, string message = "")
        {
            var data = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (data != val)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser igual a {val.ToString("MM/dd/yyyy")}." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se for igual a outra
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="val">Value to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> AreNotEquals(Expression<Func<T, string>> selector, string text, string message = "")
        {
            var val = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (val.Equals(text, StringComparison.OrdinalIgnoreCase))
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser igual a {text}." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se for igual a outra
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="val">Value to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> AreNotEquals(Expression<Func<T, int>> selector, int val, string message = "")
        {
            var data = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (data == val)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser igual a {val}." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se for igual a outra
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="val">Value to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> AreNotEquals(Expression<Func<T, decimal>> selector, decimal val, string message = "")
        {
            var data = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (data == val)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser igual a {val}." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se for igual a outra
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="val">Value to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> AreNotEquals(Expression<Func<T, double>> selector, double val, string message = "")
        {
            var data = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (data == val)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser igual a {val}." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se for igual a outra
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="val">Value to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> AreNotEquals(Expression<Func<T, bool>> selector, bool val, string message = "")
        {
            var data = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (data == val)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser igual a {val}." : message);

            return this;
        }

        /// <summary>
        ///   Dada uma string, adicione uma notificação se for igual a outra
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="val">Value to be compared</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> AreNotEquals(Expression<Func<T, DateTime>> selector, DateTime val, string message = "")
        {
            var data = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (data == val)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser igual a {val.ToString("MM/dd/yyyy")}." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se não for verdadeira
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsTrue(Expression<Func<T, bool>> selector, string message = "")
        {
            var data = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (data == false)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser verdadeira." : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se não for falso
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsFalse(Expression<Func<T, bool>> selector, string message = "")
        {
            var data = selector.Compile().Invoke(_entity);
            var name = ((MemberExpression)selector.Body).Member.Name;

            if (data == true)
                _entity.AddNotification(name, string.IsNullOrEmpty(message) ? $"Campo {name} Deve ser False" : message);

            return this;
        }

        /// <summary>
        /// Dada uma string, adicione uma notificação se não for verdadeira
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsTrue(bool value, string Campo, string message = "")
        {
            if (!value)
                _entity.AddNotification(Campo, string.IsNullOrEmpty(message) ? $"Campo {Campo} Deve ser verdadeira." : message);

            return this;
        }

        /// <summary>
        ///Dada uma string, adicione uma notificação se não for falso
        /// </summary>
        /// <param name="selector">Property</param>
        /// <param name="message">Error Message (Optional)</param>
        /// <returns></returns>
        public ValidationContract<T> IsFalse(bool value, string Campo, string message = "")
        {
            if (value == true)
                _entity.AddNotification(Campo, string.IsNullOrEmpty(message) ? $"Campo {Campo} Deve ser False" : message);

            return this;
        }
    }
}
