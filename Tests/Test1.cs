using Microsoft.VisualStudio.TestTools.UnitTesting;

using lab3.Interfaces;
using lab3.Models;
using lab3.Core;
using lab3.Services;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace Tests
{
   



        [TestClass]
        public sealed class JsonGameLoaderTests
        {
            private const string TestFilePath = "test_game_data.json";

            // Удаление тестового файла после каждого теста
            [TestCleanup]
            public void Cleanup()
            {
                if (File.Exists(TestFilePath))
                {
                    File.Delete(TestFilePath);
                }
            }

            [TestMethod]
            public void Load_ValidJsonFile_ReturnsCorrectGameData()
            {
                // Arrange
                var expectedGame = new Game
                {
                    Name = "Тестовая Игра",
                    StartSceneId = 10,
                    Scenes = new List<Scene>
                {
                    new Scene { Id = 10, Text = "Начало", Answers = new List<Answer>() }
                }
                };
                string jsonContent = JsonSerializer.Serialize(expectedGame);
                File.WriteAllText(TestFilePath, jsonContent);

                var loader = new JsonGameLoader(TestFilePath);

                // Act
                var actualGame = loader.Load();

                // Assert
                Assert.IsNotNull(actualGame, "Игра не должна быть null.");
                Assert.AreEqual(expectedGame.Name, actualGame.Name, "Имя игры должно совпадать.");
                Assert.AreEqual(expectedGame.StartSceneId, actualGame.StartSceneId, "Начальная сцена должна совпадать.");
                Assert.AreEqual(1, actualGame.Scenes.Count, "Количество сцен должно совпадать.");
                Assert.AreEqual(expectedGame.Scenes.First().Id, actualGame.Scenes.First().Id, "ID сцены должен совпадать.");
            }

            [TestMethod]
            [ExpectedException(typeof(FileNotFoundException))]
            public void Load_FileDoesNotExist_ThrowsFileNotFoundException()
            {
                // Arrange
                string nonExistentPath = "non_existent_file.json";
                if (File.Exists(nonExistentPath))
                {
                    File.Delete(nonExistentPath);
                }

                var loader = new JsonGameLoader(nonExistentPath);

                // Act
                loader.Load(); // Assert происходит через ExpectedException
            }

            [TestMethod]
            [ExpectedException(typeof(JsonException))]
            public void Load_InvalidJsonFormat_ThrowsJsonException()
            {
                // Arrange
                File.WriteAllText(TestFilePath, "Это невалидный JSON");

                var loader = new JsonGameLoader(TestFilePath);

                // Act
                loader.Load(); // Assert происходит через ExpectedException
            }
        }

        [TestClass]
        public sealed class TxtToGameConverterTests
        {
            private const string TestTxtPath = "test_input.txt";
            private const string TestJsonPath = "test_output.json";

            // Удаление тестовых файлов после каждого теста
            [TestCleanup]
            public void Cleanup()
            {
                if (File.Exists(TestTxtPath))
                {
                    File.Delete(TestTxtPath);
                }
                if (File.Exists(TestJsonPath))
                {
                    File.Delete(TestJsonPath);
                }
            }


            [TestMethod]
            public void ConvertAndSave_EmptyTxtFile_CreatesEmptyGame()
            {
                // Arrange
                string gameName = "Пустая Игра";
                File.WriteAllText(TestTxtPath, string.Empty);
                var converter = new TxtToGameConverter();

                // Act
                converter.ConvertAndSave(TestTxtPath, TestJsonPath, gameName);

                // Assert
                string jsonOutput = File.ReadAllText(TestJsonPath);
                var game = JsonSerializer.Deserialize<Game>(jsonOutput);

                Assert.AreEqual(0, game.Scenes.Count, "Не должно быть сцен в пустом файле.");
            }
        }
    }
