# Formules EMF — Référence complète

## Source : Manuel PDF EMFPlanner 7.1 (76 pages) + ITU-T K.70

---

## 1. Puissance effective rayonnée

### EIRP
```
EIRP = P_TX × G    [W]
G = 10^(G_dBi / 10)
```

### Puissance atténuée (eq.1 du manuel)
```
PTXAM = P_TX × F_TDC × F_PP × α24    [W]
```
| Facteur | Description | Valeurs typiques |
|---|---|---|
| F_TDC | Cycle de service (duty cycle) | 1.0 (FDD), ~0.75 (TDD) |
| F_PP | Réduction statistique puissance | 0.1–1.0 selon service |
| α24 | Atténuation moyenne 24h | 0.5–1.0 |

---

## 2. Champ électrique

### Source ponctuelle (ITU-T K.70)
```
E = √(30 × EIRP) / d    [V/m]
```
- d = distance 3D source↔point [m]

### Avec diagramme d'antenne
```
E(θ,φ) = √(30 × PTXAM × G(θ,φ)) / d    [V/m]
```
- G(θ,φ) = gain en fonction de l'azimut θ et élévation φ (depuis pattern)

### Champ total multi-sources (somme quadratique)
```
E_total = √(Σ Eᵢ²)    [V/m]
```

---

## 3. Limites d'exposition

### Formule générale
```
Limit(f) = E_ref × f^A    [V/m]
```
- f = fréquence [MHz]
- E_ref et A varient par bande et par standard (ICNIRP, FCC, etc.)

### Limites ICNIRP (public, 2020)
| Bande de fréquence | Limite E (V/m) |
|---|---|
| 1–10 MHz | 83 / √f |
| 10–400 MHz | 28 |
| 400–2000 MHz | 1.375 × √f |
| 2–300 GHz | 61 |

---

## 4. Exposition simultanée
```
R = Σ (Eᵢ / Limitᵢ)²
```
- Eᵢ = champ de la source i
- Limitᵢ = limite à la fréquence de la source i
- **Conforme si R < 1**

---

## 5. Zone / Volume de sécurité

### Distance de sécurité (2D)
```
d_safe = √(30 × EIRP) / E_limit    [m]
```

### Volume de sécurité (parallélépipède 3D)
Dimensions : Lh (horizontal), Lm1 / Lm2 (latéral), Lv (vertical)
Calculées à partir du diagramme d'antenne + limite.

---

## 6. Modèles de propagation

### Espace libre (FSPL)
```
L = 32.44 + 20·log₁₀(d_km) + 20·log₁₀(f_MHz)    [dB]
```

### COST-Hata (150–2000 MHz, d > 1 km, milieu urbain)
```
L = 46.3 + 33.9·log₁₀(f) - 13.82·log₁₀(h_b) - a(h_m) + (44.9 - 6.55·log₁₀(h_b))·log₁₀(d)
a(h_m) = (1.1·log₁₀(f) - 0.7)·h_m - (1.56·log₁₀(f) - 0.8)
```

### ITU-R P.1546 (simplifié)
Correction hauteur + affaiblissement espace libre. Version complète : courbes empiriques 100 MHz / 600 MHz / 2 GHz.

---

## 7. Coefficients supplémentaires

### Réflexion sol
```
k = 1.0 à 1.6    (sans dimension)
```
Appliqué au champ E comme multiplicateur.

### Atténuation bâtiment / obstacle
Valeur en dB, soustraite du champ calculé.

---

## 8. Diagrammes d'antenne

### Format MSI Planet (.msi / .pln)
Fichiers de patterns H et V, utilisables pour G(θ,φ).

### ITU-R F.699-8
Antennes à symétrie de révolution — formule de synthèse pour le gain hors-axe.

---

## Statut d'implémentation (appclone)

| Formule | Statut |
|---|---|
| EIRP | ✅ Implémenté |
| E source ponctuelle | ✅ Implémenté |
| E total multi-sources | ✅ Implémenté |
| Distance de sécurité | ✅ Implémenté |
| FSPL | ✅ Implémenté |
| COST-Hata | ✅ Implémenté |
| PTXAM (facteurs atténuation) | 🔴 À faire |
| Limites ICNIRP par bande | 🔴 À faire |
| Exposition simultanée R | 🔴 À faire |
| Volume de sécurité 3D | 🔴 À faire |
| Diagramme d'antenne G(θ,φ) | 🟡 Basique (interpolation linéaire) |
| Réflexion sol (k) | 🔴 À faire |
| ITU-R P.1546 complet | 🔴 À faire |
| ITU-R F.699-8 | 🔴 À faire |
| Atténuation bâtiments | 🔴 À faire |
