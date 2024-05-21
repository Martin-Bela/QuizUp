using QuizUp.DAL.Entities;
using Riok.Mapperly.Abstractions;
using QuizUp.Common.Models;

namespace QuizUp.BL.Mappers;

[Mapper]
public static partial class GameMapper
{
    [MapProperty("Quiz.Title", nameof(GameSummaryModel.Title))]
    public static partial GameSummaryModel MapToGameSummaryModel(this Game game);

    [MapProperty("Quiz.Title", nameof(CreateGameResultModel.Title))]
    public static partial CreateGameResultModel MapToGameCreateResultModel(this Game game);
}
