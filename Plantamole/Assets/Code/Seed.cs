using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeedType { Carrot, Potato, Onion, Beetroot, Ginger }

public class Seed : Item {

    private SeedType t;

    public Seed(SeedType type) {
        t = type;
    }
}