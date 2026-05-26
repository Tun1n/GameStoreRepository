namespace GameStore.Application.DTO.GameDTO
{
    public record GamePatchDTO
    (
    string? Name,
    string? ImageURL,
    bool? IsInstalled );
}
