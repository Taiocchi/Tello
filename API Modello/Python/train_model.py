from tensorflow.keras.models import Sequential
from tensorflow.keras.layers import Dense, Conv2D, Flatten, MaxPooling2D, Dropout
from tensorflow.keras.preprocessing.image import ImageDataGenerator
from tensorflow.keras.callbacks import ModelCheckpoint, EarlyStopping

# Percorsi al dataset
train_dir = 'dataset/train'
val_dir = 'dataset/val'

#Data augmentation per il training
train_datagen = ImageDataGenerator(
    rescale=1./255,
    rotation_range=10,       # rotazioni fino a ±10°
    width_shift_range=0.1,   # traslazioni orizzontali fino al 10%
    height_shift_range=0.1,  # traslazioni verticali fino al 10%
    zoom_range=0.1           # zoom ±10%
)

val_datagen = ImageDataGenerator(rescale=1./255)

# Generatori
train_gen = train_datagen.flow_from_directory(
    train_dir, target_size=(64, 64), class_mode='categorical'
)
val_gen = val_datagen.flow_from_directory(
    val_dir, target_size=(64, 64), class_mode='categorical'
)

#Modello
model = Sequential([
    Conv2D(32, (3, 3), activation='relu', input_shape=(64, 64, 3)),
    MaxPooling2D(pool_size=(2, 2)),

    Conv2D(64, (3, 3), activation='relu'),
    MaxPooling2D(pool_size=(2, 2)),

    Flatten(),
    Dense(128, activation='relu'),
    Dropout(0.3),   # riduce overfitting
    Dense(train_gen.num_classes, activation='softmax')
])

model.compile(optimizer='adam', loss='categorical_crossentropy', metrics=['accuracy'])

# Callback: salva solo il modello migliore + early stopping
checkpoint = ModelCheckpoint(
    "best_model.keras", monitor="val_accuracy",
    save_best_only=True, mode="max", verbose=1
)
early_stop = EarlyStopping(
    monitor="val_accuracy", patience=5, restore_best_weights=True
)

#Allenamento (con early stopping)
history = model.fit(
    train_gen,
    validation_data=val_gen,
    epochs=50,                
    callbacks=[checkpoint, early_stop]
)

# Esporta anche in formato TensorFlow SavedModel (per ONNX)
model.export("model_tf")
