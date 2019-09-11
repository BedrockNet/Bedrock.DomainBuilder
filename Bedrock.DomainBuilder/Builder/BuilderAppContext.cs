using System.Threading.Tasks;
using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder.Builder
{
    public class BuilderAppContext : BuilderBase, IBuilder
    {
        #region Protected Methods
        protected override async Task<int> BuildInternal(BuildSettings settings)
        {
            BuilderFileCountUpdate(1);

            await Pause.WaitWhilePausedAsync();

            var filePath = string.Concat(settings.AppContextPath, settings.Domain, SchemaCamelCased, "Context", settings.FileExtension);

            await BuildFile<eBuildSectionAppContext>(filePath, null, settings);

            return 1;
        }
        #endregion
    }
}
