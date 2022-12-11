# botmichael
Собрать docker image - из директории солюшена запустить ```docker build --tag michaelbot -f .\src\BotMichael.Host\Dockerfile .```
Запустить контейнер (не забудьте указать токен) - ```docker run -e "BOT_TOKEN=5677555837:AAHgxvNtTb4DFEdQig1VOmjbMUxmru-5AXco" michaelbot```
При разработке можно указать переменную окружения BOT_TOKEN в launchSettings.json