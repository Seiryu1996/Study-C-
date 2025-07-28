# Study C#

## Build & Run (Docker)

1. イメージをビルドします

    ```sh
    docker build -t web .
    ```

2. コンテナを起動します
   ※アプリはコンテナ内でポート8080でリッスンします。ホストの5252番でアクセスしたい場合は下記のように指定してください。

    ```sh
    docker run -d -p 5252:8080 --name web-container web
    ```

3. ブラウザでアクセス

    ```
    http://localhost:5252
    ```

4. コンテナを停止・削除する場合

    ```sh
    docker stop web-container
    docker rm web-container
    ```

---

## 機能

- Todoリスト
- 日記（天気情報取得機能付き）

---

## 注意

- プロジェクトパスに日本語や `#` などの記号が含まれている場合、Dockerビルドでエラーになることがあります。
  その場合は英数字のみのパスに移動

