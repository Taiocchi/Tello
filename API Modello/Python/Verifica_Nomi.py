from tensorflow.keras.preprocessing.image import ImageDataGenerator

train_dir = 'dataset/train'

datagen = ImageDataGenerator(rescale=1./255)
train_gen = datagen.flow_from_directory(
    train_dir, target_size=(64, 64), class_mode='categorical')

print(train_gen.class_indices)

labels = [None] * len(train_gen.class_indices)
for label, index in train_gen.class_indices.items():
    labels[index] = label

print(labels)  # ['gatto', 'cane', 'auto']
