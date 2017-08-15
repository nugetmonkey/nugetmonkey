using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NugetMonkey.VsExtension;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NugetMonkeyVsExtension
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class NugetMonkeyControl : UserControl
    {
        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        public NugetMonkeyControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "NugetMonkey");
        }
        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var txt = txtSearch.Text;
            if (!string.IsNullOrWhiteSpace(txt))
            {
                HttpClientRequestHandler c = new HttpClientRequestHandler();
                var r = c.GetReleases("http://search.maven.org/solrsearch/select?q=" + txtSearch.Text + "&rows=99999&wt=json");
                grdSearchResults.ItemsSource = r.response.docs;
            }else
            {
                MessageBox.Show("Please enter some texts");
            }
        }

        private void btnInstall_Click(object sender, RoutedEventArgs e)
        {
            var project = Utils.GetSelectedProject();
            if (project!=null)
            {

                string filePath = project.Properties.Item("FullPath").Value.ToString();
                var depFile = filePath + "\\AdditionalJavaDependencies.json";
                AdditionalDeps deps;
                if (File.Exists(depFile))
                {
                    deps = JsonConvert.DeserializeObject<AdditionalDeps>(File.ReadAllText(depFile), settings);
                    if (deps.AdditionalProjectDependencies  == null)
                    {
                        deps.AdditionalProjectDependencies = new List<string>(); 
                    }
                }else
                {
                    deps = new AdditionalDeps() { AdditionalProjectDependencies = new List<string>() };
                }
                var items = grdSearchResults.SelectedItems;
                if (items != null)
                {
                    foreach (Doc item in items)
                    {
                        if (!deps.AdditionalProjectDependencies.Where(d => d.ToLowerInvariant().StartsWith(item.id.ToLowerInvariant() + ":")).Any())
                        {
                            deps.AdditionalProjectDependencies.Add(item.id + ":" + item.latestVersion);
                        }
                    }
                    File.WriteAllText(depFile, JsonConvert.SerializeObject(deps, settings));
                }
                MessageBox.Show(filePath);
            }
            else
            {
                MessageBox.Show("No project is selected!");
            }
        }
    }
}
