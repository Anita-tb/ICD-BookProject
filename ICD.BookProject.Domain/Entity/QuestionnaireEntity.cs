using System.Collections.Generic;
using ICD.Framework.Domain;

namespace ICD.BookProject.Domain.Entity;

public class QuestionnaireEntity : BaseEntity<int>
{
    public string Title { get; set; }
    public string Description { get; set; }

    #region Navigation Property

    public ICollection<QuestionEntity> Questions { get; set; }

    #endregion
}