using Microsoft.Xna.Framework;
namespace GameProject.Interfaces 
{
    public interface IProjectile : ISprite
    {
        bool isExpired { get; }
        void Instantiate(Vector2 startPosition, Vector2 direction, float velocity, float lifetime);
    }
}