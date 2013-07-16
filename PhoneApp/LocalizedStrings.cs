using PinNotes.Resources;

namespace PinNotes
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        public LocalizedStrings()
        {
        }

        private static StringResources _localizedResources = new StringResources();
        public StringResources LocalizedResources { get { return _localizedResources; } }


        private static lirum localizedResourceslirum = new lirum();
        public lirum LocalizedResourceslirum { get { return localizedResourceslirum; } }
    }
}