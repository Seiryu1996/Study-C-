# Google Cloud Run デプロイ手順

## 初期設定

1. **gcloud CLIの初期化**

    ```sh
    gcloud init
    ```
    - 指示に従い、プロジェクトやアカウントを選択してください。

2. **Cloud Build APIの有効化**

    ```sh
    gcloud services enable cloudbuild.googleapis.com
    ```

3. **IAM権限の追加（必要な場合）**

    ```sh
    gcloud projects add-iam-policy-binding study-csharp \
      --member=serviceAccount:476593557263-compute@developer.gserviceaccount.com \
      --role=roles/storage.objectViewer
    ```

---

## アプリのビルドとアップロード

1. **DockerイメージをビルドしてGoogle Container Registryへアップロード**

    ```sh
    gcloud builds submit --tag gcr.io/study-csharp/my-csharp-app
    ```

---

## Cloud Run へのデプロイ

1. **Cloud Run にデプロイ**

    ```sh
    gcloud run deploy my-csharp-app \
      --image gcr.io/study-csharp/my-csharp-app \
      --platform managed \
      --region asia-northeast1 \
      --allow-unauthenticated
    ```

2. **デプロイ完了後、表示されたURLにアクセスして動作確認してください。**

---

## 補足

- プロジェクトIDやリージョンはご自身の環境に合わせて変更してください。
- `--allow-unauthenticated` を付けると誰でもアクセス可能になります。必要に応じて認証を設定してください。
