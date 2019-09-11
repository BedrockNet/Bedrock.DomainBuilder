using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Bedrock.DomainBuilder.EntityFramework;
using Bedrock.DomainBuilder.Enumerations;

namespace Bedrock.DomainBuilder.Builder
{
    #region Delegates
    public delegate void BuilderStarted(eBuilder builder);
    public delegate void BuilderInitialized(eBuilder builder);
    public delegate void BuilderBuilding(eBuilder builder);
    public delegate void BuilderFileCountUpdate(eBuilder builder, int fileCount);
    public delegate void BuilderPassComplete(eBuilder builder, string buildSection);
    public delegate void BuilderComplete(eBuilder builder, int filesCreated, int loc);
    public delegate void BuilderWarning(BuildWarning warning);
    public delegate void FileBuilding(eBuilder builder, string file);
    public delegate void FileBuilt(eBuilder builder, string file, int loc);
    #endregion

    public interface IBuilder
    {
        #region Events
        event BuilderStarted OnBuilderStarted;
        event BuilderInitialized OnBuilderInitialized;
        event BuilderBuilding OnBuilderBuilding;
        event BuilderFileCountUpdate OnBuilderFileCountUpdate;
        event BuilderPassComplete OnBuilderPassComplete;
        event BuilderComplete OnBuilderComplete;
        event BuilderWarning OnBuilderWarning;
        event FileBuilding OnFileBuilding;
        event FileBuilt OnFileBuilt;
        #endregion

        #region Properties
        eBuilder BuilderType { get; set; }

        bool IsActive { get; set; }

        EntityHelper EntityHelper { get; set; }

        List<string> Roots { get; set; }

        List<string> Enums { get; set; }

        PauseToken Pause { get; set; }

        CancellationToken Cancellation { get; set; }
        #endregion

        #region Methods
        Task Build(BuildSettings settings);

        void BuilderWarning(string section, string message);
        #endregion
    }
}
