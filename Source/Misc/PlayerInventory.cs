using GameProject.Enums;
using GameProject.Interfaces;
using GameProject.WorldPickups;

namespace GameProject.Misc {
  public class PlayerInventory {
    private Player player;

    public IItem Melee { get; private set; }
    public IItem Sidearm { get; private set; }
    public IItem Primary { get; private set; }
    public IItem Consumable { get; private set; }

    public IItem ActiveItem { get; private set; }

    public PlayerInventory(Player player) {
      this.player = player;
    }

    public void PickupItem(IItem newItem) {
      switch (newItem.Category) {
        case ItemCategory.Sidearm:
          if (Sidearm != null) { DropItem(Sidearm); }
          Sidearm = newItem;
          ActiveItem = Sidearm;
          break;

        case ItemCategory.Primary:
          if (Primary != null) { DropItem(Primary); }
          Primary = newItem;
          ActiveItem = Primary;
          break;

        case ItemCategory.Consumable:
          if (Consumable != null) { DropItem(Consumable); }
          Consumable = newItem;
          ActiveItem = Consumable;
          break;

        case ItemCategory.Melee:
          if (Melee != null) { DropItem(Melee); }
          Melee = newItem;
          ActiveItem = Melee;
          break;
      }
    }

    private void DropItem(IItem itemToDrop) {
      IWorldPickup droppedItem = new ItemWorldPickup(itemToDrop);
      player.game.StateGame.LevelManager.CurrentLevel.AddPickup(droppedItem);
    }

    public void SwitchActiveItem(ItemCategory categoryToHold) {
      if (categoryToHold == ItemCategory.Sidearm && Sidearm != null) ActiveItem = Sidearm;
      if (categoryToHold == ItemCategory.Primary && Primary != null) ActiveItem = Primary;
      if (categoryToHold == ItemCategory.Consumable && Consumable != null) ActiveItem = Consumable;
      if (categoryToHold == ItemCategory.Melee && Melee != null) ActiveItem = Melee;
    }
  }
}
