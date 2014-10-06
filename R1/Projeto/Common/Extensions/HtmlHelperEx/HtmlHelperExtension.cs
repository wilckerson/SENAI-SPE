using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Collections;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.Mvc.Ajax;

using Common.HandleException;
using Common.Extensions.StringEx;
using Common.Extensions.IEnumerableEx;
using Common.DataAnnotations;

namespace Common.Extensions.HtmlHelperEx
{
    public static class HtmlHelperExtension
    {
        #region Conditional Helpers
        /// <summary>
        /// A helper for performing conditional IF logic using Razor
        /// </summary>
        public static MvcHtmlString If(this HtmlHelper html, bool condition, string trueString)
        {
            Ensure.Argument.NotNull(html, "html");
            return html.IfElse(condition, h => MvcHtmlString.Create(trueString), h => MvcHtmlString.Empty);
        }

        /// <summary>
        /// A helper for performing conditional IF,ELSE logic using Razor
        /// </summary>
        public static MvcHtmlString IfElse(this HtmlHelper html, bool condition, string trueString, string falseString)
        {
            Ensure.Argument.NotNull(html, "html");
            return html.IfElse(condition, h => MvcHtmlString.Create(trueString), h => MvcHtmlString.Create(falseString));
        }

        /// <summary>
        /// A helper for performing conditional IF logic using Razor
        /// </summary>
        public static MvcHtmlString If(this HtmlHelper html, bool condition, Func<HtmlHelper, MvcHtmlString> action)
        {
            Ensure.Argument.NotNull(html, "html");
            Ensure.Argument.NotNull(action, "action");
            return html.IfElse(condition, action, h => MvcHtmlString.Empty);
        }

        /// <summary>
        /// A helper for performing conditional IF,ELSE logic using Razor
        /// </summary>
        public static MvcHtmlString IfElse(this HtmlHelper html, bool condition, Func<HtmlHelper, MvcHtmlString> trueAction, Func<HtmlHelper, MvcHtmlString> falseAction)
        {
            Ensure.Argument.NotNull(html, "html");
            Ensure.Argument.NotNull(trueAction, "trueAction");
            Ensure.Argument.NotNull(falseAction, "falseAction");
            return (condition ? trueAction : falseAction).Invoke(html);
        }
        #endregion

        #region Image Helpers
        /// <summary>
        /// Returns an image element for the specified <paramref name="src"/>
        /// </summary>
        public static MvcHtmlString Image(this HtmlHelper html, string src, string alt = "", object htmlAttributes = null)
        {
            Ensure.Argument.NotNull(html, "html");
            Ensure.Argument.NotNullOrEmpty(src, "src");

            if (src.StartsWith("~"))
                src = VirtualPathUtility.ToAbsolute(src);

            var tb = new TagBuilder("img");
            tb.MergeAttribute("src", src);
            tb.MergeAttribute("alt", alt);
            tb.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// Returns an image link element for the specified parameters
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="imgSrc"></param>
        /// <param name="alt"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <param name="htmlAttributes"></param>
        /// <param name="imgHtmlAttributes"></param>
        /// <returns></returns>
        public static IHtmlString ImageLink(this HtmlHelper htmlHelper, string imgSrc, string alt, string actionName, string controllerName,
            object routeValues, object htmlAttributes, object imgHtmlAttributes)
        {
            var img = new TagBuilder("img");
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            img.Attributes["src"] = urlHelper.Content(imgSrc);
            // Don't forget that the alt attribute is required if you want to have valid HTML
            img.Attributes["alt"] = alt;
            return MvcHtmlString.Create(img.ToString(TagRenderMode.SelfClosing));
        }
        #endregion

        #region CheckboxLis Helpers
        /// <summary>
        /// Returns a checkbox for each of the provided <paramref name="items"/>.
        /// </summary>
        public static MvcHtmlString CheckBoxListFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> items, object htmlAttributes = null)
        {
            var listName = ExpressionHelper.GetExpressionText(expression);
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            items = GetCheckboxListWithDefaultValues(metaData.Model, items);
            return htmlHelper.CheckBoxList(listName, items, htmlAttributes);
        }

        /// <summary>
        /// Returns a checkbox for each of the provided <paramref name="items"/>.
        /// </summary>
        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, string listName, IEnumerable<SelectListItem> items, object htmlAttributes = null)
        {
            var container = new TagBuilder("div");
            foreach (var item in items)
            {
                var label = new TagBuilder("label");
                label.MergeAttribute("class", "checkbox"); // default class
                label.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

                var cb = new TagBuilder("input");
                cb.MergeAttribute("type", "checkbox");
                cb.MergeAttribute("name", listName);
                cb.MergeAttribute("value", item.Value ?? item.Text);
                if (item.Selected)
                    cb.MergeAttribute("checked", "checked");

                label.InnerHtml = cb.ToString(TagRenderMode.SelfClosing) + item.Text;

                container.InnerHtml += label.ToString();
            }

            return new MvcHtmlString(container.ToString());
        }

        private static IEnumerable<SelectListItem> GetCheckboxListWithDefaultValues(object defaultValues, IEnumerable<SelectListItem> selectList)
        {
            var defaultValuesList = defaultValues as IEnumerable;

            if (defaultValuesList == null)
                return selectList;

            IEnumerable<string> values = from object value in defaultValuesList
                                         select Convert.ToString(value, CultureInfo.CurrentCulture);

            var selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
            var newSelectList = new List<SelectListItem>();

            selectList.ForEach(item =>
            {
                item.Selected = (item.Value != null) ? selectedValues.Contains(item.Value) : selectedValues.Contains(item.Text);
                newSelectList.Add(item);
            });

            return newSelectList;
        }
        #endregion

        public static MvcHtmlString RadioButtonForSelectList<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> listOfValues)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var sb = new StringBuilder();

            if (listOfValues != null)
            {
                // Create a radio button for each item in the list 
                foreach (SelectListItem item in listOfValues)
                {
                    // Generate an id to be given to the radio button field 
                    var id = string.Format("{0}_{1}", metaData.PropertyName, item.Value);

                    // Create and populate a radio button using the existing html helpers 
                    var label = htmlHelper.Label(id, HttpUtility.HtmlEncode(item.Text));
                    var radio = htmlHelper.RadioButtonFor(expression, item.Value, new { id = id }).ToHtmlString();

                    // Create the html string that will be returned to the client 
                    // e.g. <input data-val="true" data-val-required="You must select an option" id="TestRadio_1" name="TestRadio" type="radio" value="1" /><label for="TestRadio_1">Line1</label> 
                    sb.AppendFormat("<div class=\"RadioButton\">{0}{1}</div>", radio, label);
                }
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        #region Textbox Helpers
        /// <summary>
        /// Returns a TextboxFor elemento for the specified parameters and Model
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString iDPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {

            var teste = new Dictionary<string, object>();

            var attributes = new RouteValueDictionary();
            var memberAccessExpression = (MemberExpression)expression.Body;
            object[] htmlAttributes = memberAccessExpression.Member.GetCustomAttributes(typeof(HtmlAttributesAttribute), true);

            if (htmlAttributes.Length > 0)
            {
                attributes.Add("maxLength", ((HtmlAttributesAttribute)htmlAttributes[0]).MaxLength);
                attributes.Add("style", String.Format("width:{0}px", ((HtmlAttributesAttribute)htmlAttributes[0]).Size));

                if (((HtmlAttributesAttribute)htmlAttributes[0]).AllowOnlyNumber)
                {
                    attributes.Add("onkeypress", "return AllowOnlyNumber(event)");
                }
            }

            return helper.PasswordFor(expression, attributes);
        }

        /// <summary>
        /// Returns a Textbox element for the specified parameters and Model and also can be configurated the behaviors: MaxLength, Size and OnlyNumber
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="helper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString iDTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {

            var teste = new Dictionary<string, object>();

            var attributes = new RouteValueDictionary();
            var memberAccessExpression = (MemberExpression)expression.Body;
            object[] htmlAttributes = memberAccessExpression.Member.GetCustomAttributes(typeof(HtmlAttributesAttribute), true);

            if (htmlAttributes.Length > 0)
            {
                attributes.Add("maxLength", ((HtmlAttributesAttribute)htmlAttributes[0]).MaxLength);
                attributes.Add("style", String.Format("width:{0}px", ((HtmlAttributesAttribute)htmlAttributes[0]).Size));

                if (((HtmlAttributesAttribute)htmlAttributes[0]).AllowOnlyNumber)
                {
                    attributes.Add("onkeypress", "return AllowOnlyNumber(event)");
                }
            }

            return helper.TextBoxFor(expression, attributes);
        }
        #endregion

        /// <summary>
        /// Returns a description for this model property
        /// </summary>
        public static IHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            Ensure.Argument.NotNull(html, "html");
            Ensure.Argument.NotNull(expression, "expression");

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return new HtmlString(metadata.Description);
        }

        /// <summary>
        /// Creates a dropdown list for an Enum property
        /// </summary>
        /// <exception cref="System.ArgumentException">If the property type is not a valid Enum</exception>
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> html, Expression<Func<TModel, TEnum>> expression)
        {
            Ensure.Argument.Is(typeof(TEnum).IsEnum, "Must be a valid Enum type.");

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            var values = from v in Enum.GetValues(typeof(TEnum)).Cast<TEnum>()
                         select new
                         {
                             Text = v.ToString().SeparatePascalCase(),
                             Value = v.ToString()
                         };

            var selectList = new SelectList(values, "Value", "Text");
            return html.DropDownListFor(expression, selectList);
        }

        /// <summary>
        /// A helper for wiring up Twitter bootstrap's Typeahead component.
        /// </summary>
        /// <param name="items">The list to be serialized as JSON and assigned to the data-items attribute of the textbox</param>
        public static MvcHtmlString TypeaheadFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<string> source, int items = 8)
        {
            Ensure.Argument.NotNull(expression, "expression");
            Ensure.Argument.NotNull(source, "source");

            var jsonString = new JavaScriptSerializer().Serialize(source);

            return htmlHelper.TextBoxFor(expression, new { autocomplete = "off", data_provide = "typeahead", data_items = items, data_source = jsonString });
        }

        /// <summary>
        /// Renders an Alert if one exists in TempData (requires a partial view named _Alert)
        /// </summary>
        public static MvcHtmlString Alert(this HtmlHelper html)
        {
            var alert = html.ViewContext.TempData[typeof(Alert).FullName] as Alert;
            if (alert != null)
                return html.Partial("_Alert", alert);

            return MvcHtmlString.Empty;
        }

        public static string AjaxImageActionLink(this AjaxHelper helper, string imageUrl, string altText, string actionName, string controller, object routeValues, AjaxOptions ajaxOptions)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttribute("alt", altText);
            var link = helper.ActionLink("[replaceme]", actionName, controller, routeValues, ajaxOptions);            
            return link.ToStringOrEmpty().Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}
